using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//�G�̎�ރ����i_enemys�̏��ԁj
//0�u�ڂ�ʁv
//1�`4�uAir/���l�v
//5�`10�uEarth/��̃S�[�����v
//11�`15�uWater/���̐���v
//16�`20�uLava/�{�X�p�E���������Ȗ��l�v

public class NewEnemyControl : MonoBehaviour
{

    #region"�ϐ��Ƃ��܂Ƃ�"

    #region"�G��WAVE��ԊǗ�"
    [SerializeField, Header("�G��WAVE��ԊǗ�")]
    EnemyPhase enemyPhase = EnemyPhase.WaveWait;
    private enum EnemyPhase{
        Wave1,Wave2,Wave3,BossWave,WaveEnd,WaveWait,TimeReset
    }
    #endregion

    [SerializeField, Header("�e��ނ̓G�������ɒǉ��i�S21��ށj")]
    private GameObject[] _enemys = default;
    [SerializeField, Header("�eWAVE�̎��ԁi���v4WAVE�j")]
    private float[] _phaseTime;
    [SerializeField, Header("�eWAVE�ɏo������G�̐��i���v4WAVE�j")]
    private int[] _numberOfEnemies;
    [SerializeField, Header("�G�t�F�[�Y�̃e�L�X�g")]
    private TextMeshPro _PhaseText;
    [SerializeField, Header("1�t�F�[�Y�̎��Ԃ̃e�L�X�g")]
    private TextMeshPro _TimeText;
    [SerializeField, Header("�J�E���g�_�E���e�L�X�g")]
    private TextMeshPro _waitText;
    [SerializeField, Header("�G�o���ꏊ")]
    private GameObject[] _enemyPos = default;
    [SerializeField, Header("��")]
    private ParticleSystem[] _recovery = default;
    [SerializeField, Header("���̃E�F�[�u�܂ł̎���")]
    private float _nextWaitTime = default;

    /// <summary>
    /// Wave�e�I�u�W�F�N�g
    /// </summary>
    private GameObject _waveBase;
    /// <summary>
    /// �eWave�I�u�W�F�N�g
    /// </summary>
    private GameObject[] _waveChilled = default;

    /// <summary>
    /// �c��^�C�����v
    /// </summary>
    private float _phaseTimeTotal;
    /// <summary>
    /// ���v�X�R�A
    /// </summary>
    private float _scoreTotal;
    /// <summary>
    /// �E�F�[�u�؂�ւ�莞��
    /// </summary>
    private float _waitTime;

    /// <summary>
    /// Wave�̍ő吔
    /// </summary>
    private int _maxEnemyCount;
    /// <summary>
    /// �J�E���g�i�߂镪
    /// </summary>
    private int _enemyValue;

    private int _enemyCount;

    private int _enemyWaveCount;

    private int _nextRe;

    /// <summary>
    /// ���݂̏��
    /// </summary>
    private int _gameState;

    //random���l
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
        //�G�̃t�F�[�Y��ԊǗ�����
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
