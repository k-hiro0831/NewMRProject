using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//敵の種類メモ（_enemysの順番）
//0「目ん玉」
//1～4「Air/魔人」
//5～10「Earth/岩のゴーレム」
//11～15「Water/水の精霊」
//16～20「Lava/ボス用・すごそうな魔人」

public class NewEnemyControl : MonoBehaviour
{

    #region"変数とかまとめ"

    #region"敵のWAVE状態管理"
    [SerializeField, Header("敵のWAVE状態管理")]
    EnemyPhase enemyPhase = EnemyPhase.WaveWait;
    private enum EnemyPhase{
        Wave1,Wave2,Wave3,BossWave,WaveEnd,WaveWait,TimeReset
    }
    #endregion

    [SerializeField, Header("各種類の敵をここに追加（全21種類）")]
    private GameObject[] _enemys = default;
    [SerializeField, Header("各WAVEの時間（合計4WAVE）")]
    private float[] _phaseTime;
    [SerializeField, Header("各WAVEに出現する敵の数（合計4WAVE）")]
    private int[] _numberOfEnemies;
    [SerializeField, Header("敵フェーズのテキスト")]
    private TextMeshPro _PhaseText;
    [SerializeField, Header("1フェーズの時間のテキスト")]
    private TextMeshPro _TimeText;
    [SerializeField, Header("カウントダウンテキスト")]
    private TextMeshPro _waitText;
    [SerializeField, Header("敵出現場所")]
    private GameObject[] _enemyPos = default;
    [SerializeField, Header("回復")]
    private ParticleSystem[] _recovery = default;
    [SerializeField, Header("次のウェーブまでの時間")]
    private float _nextWaitTime = default;

    /// <summary>
    /// Wave親オブジェクト
    /// </summary>
    private GameObject _waveBase;
    /// <summary>
    /// 各Waveオブジェクト
    /// </summary>
    private GameObject[] _waveChilled = default;

    /// <summary>
    /// 残りタイム合計
    /// </summary>
    private float _phaseTimeTotal;
    /// <summary>
    /// 合計スコア
    /// </summary>
    private float _scoreTotal;
    /// <summary>
    /// ウェーブ切り替わり時間
    /// </summary>
    private float _waitTime;

    /// <summary>
    /// Waveの最大数
    /// </summary>
    private int _maxEnemyCount;
    /// <summary>
    /// カウント進める分
    /// </summary>
    private int _enemyValue;

    private int _enemyCount;

    private int _enemyWaveCount;

    private int _nextRe;

    /// <summary>
    /// 現在の状態
    /// </summary>
    private int _gameState;

    //random数値
    private int _ran;
    private int _ranPos;

    private bool _waveEnd;

    private EnemyValueManager _enem;
    private GameFlowManager _gameFlow;



    #endregion

    #region"Start&UpdateMethod"
    void Start(){
        StartMethod();
    }

    void Update(){
        Phase();
    }
    #endregion

    private void StartMethod(){
        _waveBase = this.transform.GetChild(0).gameObject;
        _waveChilled = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            _waveChilled[i] = _waveBase.transform.GetChild(i).gameObject;
        }
        _PhaseText.text = "WAIT...";
        ObjSet();
        _waitTime = _nextWaitTime;
        _enem = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<EnemyValueManager>();
        _gameFlow = GameObject.FindGameObjectWithTag("GameFlow").GetComponent<GameFlowManager>();
    }

    private void Phase(){
        //敵のフェーズ状態管理分岐
        switch (enemyPhase)
        {
            case EnemyPhase.TimeReset:
                break;
            case EnemyPhase.Wave1:
                _phaseTime[0] -= Time.deltaTime;
                _TimeText.text = "TIME:" + Mathf.Floor(_phaseTime[0]);
                if (_enemyCount >= _maxEnemyCount)
                {
                    _waveEnd = true;
                }
                if (_phaseTime[0] == 0.0f || _phaseTime[0] <= 0.0f)
                {
                    for (int i = 0; i < _maxEnemyCount; i++)
                    {
                        if (_waveChilled[0].transform.GetChild(i).gameObject.activeSelf == true)
                        {
                            _waveChilled[0].transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    _phaseTime[0] = 0.0f;
                    enemyPhase = EnemyPhase.WaveWait;
                }
                if (_enemyWaveCount == _maxEnemyCount)
                {
                    _recovery[0].gameObject.SetActive(true);
                    _enem.ScoreAdd((int)_phaseTimeTotal);
                    enemyPhase = EnemyPhase.WaveWait;
                }
                break;
            case EnemyPhase.Wave2:
                _phaseTime[1] -= Time.deltaTime;
                _TimeText.text = "TIME:" + Mathf.Floor(_phaseTime[1]);
                if (_enemyCount >= _maxEnemyCount)
                {
                    _waveEnd = true;
                }
                if (_phaseTime[1] == 0.0f || _phaseTime[1] <= 0.0f)
                {
                    for (int i = 0; i < _maxEnemyCount; i++)
                    {
                        if (_waveChilled[1].transform.GetChild(i).gameObject.activeSelf == true)
                        {
                            _waveChilled[1].transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    _phaseTime[1] = 0.0f;
                    enemyPhase = EnemyPhase.WaveWait;
                }
                if (_enemyWaveCount == _maxEnemyCount)
                {
                    _recovery[0].gameObject.SetActive(true);
                    _enem.ScoreAdd((int)_phaseTimeTotal);
                    enemyPhase = EnemyPhase.WaveWait;
                }
                break;
            case EnemyPhase.Wave3:
                _phaseTime[2] -= Time.deltaTime;
                _TimeText.text = "TIME:" + Mathf.Floor(_phaseTime[2]);
                if (_enemyCount >= _maxEnemyCount)
                {
                    _waveEnd = true;
                }
                if (_phaseTime[2] == 0.0f || _phaseTime[2] <= 0.0f)
                {
                    for (int i = 0; i < _maxEnemyCount; i++)
                    {
                        if (_waveChilled[2].transform.GetChild(i).gameObject.activeSelf == true)
                        {
                            _waveChilled[2].transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    _phaseTime[2] = 0.0f;
                    enemyPhase = EnemyPhase.WaveWait;
                }
                if (_enemyWaveCount == _maxEnemyCount)
                {
                    _recovery[0].gameObject.SetActive(true);
                    _enem.ScoreAdd((int)_phaseTimeTotal);
                    enemyPhase = EnemyPhase.WaveWait;
                }
                break;
            case EnemyPhase.BossWave:
                _phaseTime[3] -= Time.deltaTime;
                _TimeText.text = "TIME:" + Mathf.Floor(_phaseTime[3]);
                if (_enemyCount >= _maxEnemyCount)
                {
                    _waveEnd = true;
                }
                if (_phaseTime[3] == 0.0f || _phaseTime[0] <= 0.0f)
                {
                    for (int i = 0; i < _maxEnemyCount; i++)
                    {
                        if (_waveChilled[3].transform.GetChild(i).gameObject.activeSelf == true)
                        {
                            _waveChilled[3].transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    _phaseTime[3] = 0.0f;
                    enemyPhase = EnemyPhase.WaveEnd;
                }
                if (_enemyWaveCount == _maxEnemyCount)
                {
                    _enem.ScoreAdd((int)_phaseTimeTotal);
                    _gameState = 1;
                    enemyPhase = EnemyPhase.WaveEnd;
                }
                break;
            case EnemyPhase.WaveEnd:
                _PhaseText.text = "END";
                _TimeText.text = "";
                _gameFlow.End(_gameState);
                break;
            case EnemyPhase.WaveWait:
                _waitTime -= Time.deltaTime;
                if (_PhaseText.text == "WAVE3")
                {
                    _waitText.text = "NEXT...WAVEBOSS:" + Mathf.Floor(_waitTime);
                    if (_waitTime < 0.0f)
                    {
                        _waveEnd = false;
                        _waitText.text = "";
                        _waitTime = _nextWaitTime;
                        _PhaseText.text = "WAVEBOSS";
                        _maxEnemyCount = _numberOfEnemies[3];
                        _enemyWaveCount = 0;
                        _enemyValue = 0;
                        _enemyCount = 1;
                        StartCoroutine("EnemyActivCol");
                        enemyPhase = EnemyPhase.BossWave;
                    }
                }
                if (_PhaseText.text == "WAVE2")
                {
                    _waitText.text = "NEXT...WAVE3:" + Mathf.Floor(_waitTime);
                    if (_waitTime < 0.0f)
                    {
                        _waveEnd = false;
                        _waitText.text = "";
                        _waitTime = _nextWaitTime;
                        _PhaseText.text = "WAVE3";
                        _maxEnemyCount = _numberOfEnemies[2];
                        _enemyWaveCount = 0;
                        _enemyValue = 0;
                        _enemyCount = 10;
                        StartCoroutine("EnemyActivCol");
                        enemyPhase = EnemyPhase.Wave3;
                    }
                }
                if (_PhaseText.text == "WAVE1")
                {
                    _waitText.text = "NEXT...WAVE2:" + Mathf.Floor(_waitTime);
                    if (_waitTime < 0.0f)
                    {
                        _waveEnd = false;
                        _waitText.text = "";
                        _waitTime = _nextWaitTime;
                        _PhaseText.text = "WAVE2";
                        _maxEnemyCount = _numberOfEnemies[1];
                        _enemyWaveCount = 0;
                        _enemyValue = 0;
                        _enemyCount = 10;
                        StartCoroutine("EnemyActivCol");
                        enemyPhase = EnemyPhase.Wave2;
                    }
                }
                if (_PhaseText.text == "WAIT...")
                {
                    _waitText.text = "NEXT...WAVE1:" + Mathf.Floor(_waitTime);
                    if (_waitTime < 0.0f)
                    {
                        _waveEnd = false;
                        _waitText.text = "";
                        _waitTime = _nextWaitTime;
                        _PhaseText.text = "WAVE1";
                        _maxEnemyCount = _numberOfEnemies[0];
                        _enemyWaveCount = 0;
                        _enemyValue = 0;
                        _enemyCount = 10;
                        StartCoroutine("EnemyActivCol");
                        enemyPhase = EnemyPhase.Wave1;
                    }
                }
                break;
        }

        if (_gameState == 2)
        {
            enemyPhase = EnemyPhase.WaveEnd;
        }
    }

    private void ObjSet()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < _numberOfEnemies[i]; j++)
            {
                if (i == 3)
                {
                    _ran = Random.Range(16, 20);
                    _ranPos = Random.Range(0, 13);
                }
                else
                {
                    _ran = Random.Range(0, 15);
                    _ranPos = Random.Range(0, 13);
                }
                GameObject obj = GameObject.Instantiate(_enemys[_ran], _enemyPos[_ranPos].transform.position, Quaternion.identity);
                obj.transform.parent = _waveChilled[i].gameObject.transform;
                obj.SetActive(false);
            }
        }
    }

    private void WaveEnemy()
    {

    }

    private IEnumerator EnemyActivCol()
    {
        while (true)
        {
            if (_waveEnd)
            {
                yield break;
            }
            Puls();
            if (_PhaseText.text == "WAVE1")
            {
                yield return new WaitForSeconds(20.0f);
            }
            if (_PhaseText.text == "WAVE2")
            {
                yield return new WaitForSeconds(15.0f);
            }
            if (_PhaseText.text == "WAVE3")
            {
                yield return new WaitForSeconds(10.0f);
            }
            if (_PhaseText.text == "WAVEBOSS")
            {
                _waveEnd = true;
            }
            if (_PhaseText.text != "WAVEBOSS")
            {
                _enemyValue = _enemyValue + 10;
                _enemyCount = _enemyCount + 10;
            }

        }

    }

    private void Puls()
    {
        if (_PhaseText.text == "WAVE1")
        {
            for (int j = _enemyValue; j < _enemyCount; j++)
            {
                _waveChilled[0].transform.GetChild(j).gameObject.SetActive(true);
            }
        }
        if (_PhaseText.text == "WAVE2")
        {
            for (int j = _enemyValue; j < _enemyCount; j++)
            {
                _waveChilled[1].transform.GetChild(j).gameObject.SetActive(true);
            }
        }
        if (_PhaseText.text == "WAVE3")
        {
            for (int j = _enemyValue; j < _enemyCount; j++)
            {
                _waveChilled[2].transform.GetChild(j).gameObject.SetActive(true);
            }
        }
        if (_PhaseText.text == "WAVEBOSS")
        {
            for (int j = _enemyValue; j < _enemyCount; j++)
            {
                _waveChilled[3].transform.GetChild(j).gameObject.SetActive(true);
            }
        }

    }

    public void EnemyValueMinus(int _value)
    {
        _enemyValue = _enemyValue - _value;
    }

    public void EnemyWaveCount(int _value)
    {
        _enemyWaveCount += _value;
    }

    public void GameState(int _value)
    {
        _gameState = _value;
    }

}
