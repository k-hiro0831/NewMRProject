using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemControl : MonoBehaviour{

    #region"variable"
    [SerializeField, Header("�G�̃t�F�[�Y��ԊǗ�")]
    EnemyPhase enemyPhase = EnemyPhase.phaseWait;

    /// <summary>
    /// �c��^�C�����v
    /// </summary>
    [SerializeField, Header("�c��^�C�����v")]
    private float _phaseTimeTotal;

    /// <summary>
    /// ���v�X�R�A
    /// </summary>
    private float _scoreTotal;

    /// <summary>
    /// 1�t�F�[�Y�̎���(1�t�F�[�Y100�b�A���v300�b�z��)
    /// �J�E���g�_�E��
    /// </summary>
    [SerializeField, Header("1�t�F�[�Y�̎���")]
    private float[] _phaseTime;

    /// <summary>
    /// �ҋ@����
    /// </summary>
    private float _waitTime;

    /// <summary>
    /// 1�t�F�[�Y���Ƃ̏I��蔻��
    /// </summary>
    private bool _phaseJudge;

    /// <summary>
    /// �G�������
    /// </summary>
    private bool _enemyExchange;

    /// <summary>
    /// �e�t�F�[�Y�̃E�F�[�u���i�[
    /// </summary>
    private int _enemyPhaseNumber;

    /// <summary>
    /// ���݂̏��
    /// </summary>
    [SerializeField]
    private int _gameState;

    [SerializeField,Header("�e��ނ̓G�A���Ԃɏo�Ă���(14)")]
    private GameObject[] _enemys = default;
    [SerializeField, Header("�G�̐����ʒu�E�e�I�u�W�F�N�g")]
    private GameObject _positionBase = default;
    [SerializeField, Header("�G�̐����ʒu(potision)")]
    private GameObject[] _position = default;
    [SerializeField, Header("�G�̃t�F�[�Y�ʂ̐�(14)")]
    private int[] _enemysNumber = default;
    [SerializeField]
    private List<GameObject> _enemyList;
    [SerializeField, Header("�e�I�u�W�F�N�g�̑�{�ɂȂ���")]
    private GameObject _parentBase;
    private GameObject[] _parent = default;
    [SerializeField, Header("�G�t�F�[�Y�̃e�L�X�g")]
    private TextMeshPro _PhaseText;
    [SerializeField, Header("1�t�F�[�Y�̎��Ԃ̃e�L�X�g")]
    private TextMeshPro _TimeText;
    [SerializeField, Header("�J�E���g�_�E���e�L�X�g")]
    private TextMeshPro _waitText;

    /// <summary>
    /// �G�����ʒu
    /// </summary>
    private int _enemyPos;

    private int _enemyPos2;

    private int _enemyPosPuls;

    private EnemyManager _enemydes;

    private EnemyValueManager _enem;

    private GameFlowManager _gameFlow;

    #region"�t�F�[�Y���Ƃ̓G"
    private ObjectPool _poolPhase1;
    private ObjectPool _poolPhase2;
    private ObjectPool _poolPhase3;

    private ObjectPool _poolPhase4;
    private ObjectPool _poolPhase5;
    private ObjectPool _poolPhase6;
    private ObjectPool _poolPhase7;
    private ObjectPool _poolPhase8;

    private ObjectPool _poolPhase9;
    private ObjectPool _poolPhase10;
    private ObjectPool _poolPhase11;
    private ObjectPool _poolPhase12;
    private ObjectPool _poolPhase13;
    private ObjectPool _poolPhase14;

    private ObjectPool _poolPhaseEND;
    #endregion

    /// <summary>
    /// �G�̃t�F�[�Y��ԊǗ�
    /// </summary>
    private enum EnemyPhase {
        phase1,phase2,phase3, phase4,phaseEnd, phaseWait
    }
    #endregion

    private void Start(){
        StartMethod();
    }

    private void Update(){
        if (_gameState == 2)
        {
            enemyPhase = EnemyPhase.phaseEnd;
        }
    }

    private void FixedUpdate(){
        Phase();  
    }

    private void Phase()
    {
        //�G�̃t�F�[�Y��ԊǗ�����
        switch (enemyPhase)
        {
            case EnemyPhase.phase1:
                _phaseTime[0] -= Time.deltaTime;
                _TimeText.text = "LIMIT:"+ Mathf.Floor(_phaseTime[0]);
                if (_phaseTime[0] == 0.0f || _phaseTime[0] <= 0.0f)
                {
                    for (int i = 0; i< _enemyList.Count;i++)
                    {
                        _enemydes = _enemyList[i].GetComponent<EnemyManager>();
                        _enemydes.EnemyRemove();
                    }
                    _phaseTime[0] = 0.0f;
                    _TimeText.text = "LIMIT:" + Mathf.Floor(_phaseTime[0]);
                    _enemyPosPuls = _enemysNumber[0] + _enemysNumber[1] + _enemysNumber[2];
                    _PhaseText.text = "WAVE2";
                    enemyPhase = EnemyPhase.phaseWait;
                    _phaseJudge = false;
                    PhaseReset();
                }
                if (_enemyList.Count == 0)
                {
                    EnemyManagement();
                }
                break;
            case EnemyPhase.phase2:
                _phaseTime[1] -= Time.deltaTime;
                _TimeText.text = "LIMIT:" + Mathf.Floor(_phaseTime[1]);
                if (_phaseTime[1] == 0.0f || _phaseTime[1] <= 0.0f)
                {
                    for (int i = 0; i < _enemyList.Count; i++)
                    {
                        _enemydes = _enemyList[i].GetComponent<EnemyManager>();
                        _enemydes.EnemyRemove();
                    }
                    _phaseTime[1] = 0.0f;
                    _TimeText.text = "LIMIT:" + Mathf.Floor(_phaseTime[1]);
                    _enemyPosPuls = _enemysNumber[3] + _enemysNumber[4] + _enemysNumber[5] + _enemysNumber[6] + _enemysNumber[7];
                    _PhaseText.text = "WAVE3";
                    enemyPhase = EnemyPhase.phaseWait;
                    _phaseJudge = false;
                    PhaseReset();
                }
                if (_enemyList.Count == 0)
                {
                    EnemyManagement();
                }
                break;
            case EnemyPhase.phase3:

                _phaseTime[2] -= Time.deltaTime;
                _TimeText.text = "LIMIT:" + Mathf.Floor(_phaseTime[2]);
                if (_phaseTime[2] == 0.0f || _phaseTime[2] <= 0.0f)
                {
                    for (int i = 0; i < _enemyList.Count; i++)
                    {
                        _enemydes = _enemyList[i].GetComponent<EnemyManager>();
                        _enemydes.EnemyRemove();
                    }
                    _phaseTime[2] = 0.0f;
                    _TimeText.text = "LIMIT:" + Mathf.Floor(_phaseTime[2]);
                    _PhaseText.text = "BOSSWAVE";
                    enemyPhase = EnemyPhase.phaseWait;
                    _phaseJudge = false;
                    PhaseReset();
                }
                if (_enemyList.Count == 0)
                {
                    EnemyManagement();
                }
                break;
            case EnemyPhase.phase4:
                _phaseTime[3] -= Time.deltaTime;
                _TimeText.text = "LIMIT:" + Mathf.Floor(_phaseTime[3]);
                if (_phaseTime[3] == 0.0f || _phaseTime[3] <= 0.0f)
                {
                    for (int i = 0; i < _enemyList.Count; i++)
                    {
                        _enemydes = _enemyList[i].GetComponent<EnemyManager>();
                        _enemydes.EnemyRemove();
                    }
                    _phaseTime[3] = 0.0f;
                    _TimeText.text = "LIMIT:" + Mathf.Floor(_phaseTime[3]);
                    _PhaseText.text = "END";
                    _gameState = 2;
                    enemyPhase = EnemyPhase.phaseEnd;
                    //_phaseJudge = false;
                    //PhaseReset();
                }
                if (_enemyList.Count == 0)
                {
                    EnemyManagement();
                }
                break;
            case EnemyPhase.phaseEnd:
                _PhaseText.text = "END";
                _TimeText.text = "";
                _gameFlow.End(_gameState);
                break;
            case EnemyPhase.phaseWait:
                _waitTime -= Time.deltaTime;
                if (_PhaseText.text == "WAVE1")
                {
                    _waitText.text = "WAVE1:" + Mathf.Floor(_waitTime);
                    if (_waitTime < 0.0f)
                    {
                        _waitText.text = "";
                        _waitTime = 11.0f;
                        enemyPhase = EnemyPhase.phase1;
                        //���Z�b�g
                        PhaseReset();
                    }
                }
                if (_PhaseText.text == "WAVE2:")
                {
                    _waitText.text = "WAVE2" + Mathf.Floor(_waitTime);
                    if (_waitTime < 0.0f)
                    {
                        _waitText.text = "";
                        _waitTime = 11.0f;
                        enemyPhase = EnemyPhase.phase2;
                        //���Z�b�g
                        PhaseReset();
                    }
                }
                if (_PhaseText.text == "WAVE3:")
                {
                    _waitText.text = "WAVE3" + Mathf.Floor(_waitTime);
                    if (_waitTime < 0.0f)
                    {
                        _waitText.text = "";
                        _waitTime = 11.0f;
                        enemyPhase = EnemyPhase.phase3;
                        //���Z�b�g
                        PhaseReset();
                    }
                }
                if (_PhaseText.text == "BossWAVE")
                {
                    _waitText.text = "BOSSWAVE:" + Mathf.Floor(_waitTime);
                    if (_waitTime < 0.0f)
                    {
                        _waitText.text = "";
                        _waitTime = 11.0f;
                        enemyPhase = EnemyPhase.phase4;
                        //���Z�b�g
                        PhaseReset();
                    }
                }
                break;
        }
    }

    private void StartMethod() {
        _enemyList = new List<GameObject>();
        _poolPhase1 = gameObject.AddComponent<ObjectPool>();
        _poolPhase2 = gameObject.AddComponent<ObjectPool>();
        _poolPhase3 = gameObject.AddComponent<ObjectPool>();

        _poolPhase4 = gameObject.AddComponent<ObjectPool>();
        _poolPhase5 = gameObject.AddComponent<ObjectPool>();
        _poolPhase6 = gameObject.AddComponent<ObjectPool>();
        _poolPhase7 = gameObject.AddComponent<ObjectPool>();
        _poolPhase8 = gameObject.AddComponent<ObjectPool>();

        _poolPhase9 = gameObject.AddComponent<ObjectPool>();
        _poolPhase10 = gameObject.AddComponent<ObjectPool>();
        _poolPhase11 = gameObject.AddComponent<ObjectPool>();
        _poolPhase12 = gameObject.AddComponent<ObjectPool>();
        _poolPhase13 = gameObject.AddComponent<ObjectPool>();
        _poolPhase14 = gameObject.AddComponent<ObjectPool>();

        _poolPhaseEND = gameObject.AddComponent<ObjectPool>();

        for (int x = 0;x < _position.Length;x++){
            _position[x] = _positionBase.transform.GetChild(x).gameObject;   
        }
        _waitTime = 11.0f;
        //�e�I�u�W�F�N�g�Ɋe�G��o�^
        Parent();
        //�v�[���ɂɊe�G��o�^
        PoolSet();
        //�E�F�[�u�̓G�̐���������
        _enemyPos2 = _enemyPosPuls;
        //�e�L�X�g��ύX
        _PhaseText.text = "WAVE1";
        enemyPhase = EnemyPhase.phaseWait;
        _enem = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<EnemyValueManager>();
        _gameFlow = GameObject.FindGameObjectWithTag("GameFlow").GetComponent<GameFlowManager>();
    }

    private void EnemyManagement()
    {
        if (enemyPhase == EnemyPhase.phase4 && _enemyPhaseNumber == 0)
        {
            _phaseTimeTotal += _phaseTime[3];
            _enem.ScoreAdd((int)_phaseTimeTotal);
            _gameState = 1;
            enemyPhase = EnemyPhase.phaseEnd;
            //_phaseJudge = false;
            //PhaseReset();
        }
        if (enemyPhase == EnemyPhase.phase4 && _enemyPhaseNumber == 1)
        {
            _enemyPos2 = _enemyPosPuls;
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }

        #region"�t�F�[�Y�R"
        if (enemyPhase == EnemyPhase.phase3 && _enemyPhaseNumber == 1)
        {
            _phaseTimeTotal += _phaseTime[2];
            _enem.ScoreAdd((int)_phaseTimeTotal);
            _PhaseText.text = "BOSSWAVE";
            enemyPhase = EnemyPhase.phaseWait;
            _phaseJudge = false;
            PhaseReset();
        }
        if (enemyPhase == EnemyPhase.phase3 && _enemyPhaseNumber == 2)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase14();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        if (enemyPhase == EnemyPhase.phase3 && _enemyPhaseNumber == 3)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase13();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        if (enemyPhase == EnemyPhase.phase3 && _enemyPhaseNumber == 4)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase12();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        if (enemyPhase == EnemyPhase.phase3 && _enemyPhaseNumber == 5)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase11();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        if (enemyPhase == EnemyPhase.phase3 && _enemyPhaseNumber == 6)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase10();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        #endregion

        #region"�t�F�[�Y�Q"
        if (enemyPhase == EnemyPhase.phase2 && _enemyPhaseNumber == 1)
        {
            _phaseTimeTotal += _phaseTime[1];
            _enem.ScoreAdd((int)_phaseTimeTotal);
            _PhaseText.text = "WAVE3";
            enemyPhase = EnemyPhase.phaseWait;
            _phaseJudge = false;
            PhaseReset();
        }
        if (enemyPhase == EnemyPhase.phase2 && _enemyPhaseNumber == 2)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase8();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        if (enemyPhase == EnemyPhase.phase2 && _enemyPhaseNumber == 3)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase7();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        if (enemyPhase == EnemyPhase.phase2 && _enemyPhaseNumber == 4)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase6();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        if (enemyPhase == EnemyPhase.phase2 && _enemyPhaseNumber == 5)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase5();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        #endregion

        #region"�t�F�[�Y�P"
        if (enemyPhase == EnemyPhase.phase1 && _enemyPhaseNumber == 1)
        {
            _phaseTimeTotal += _phaseTime[0];
            _enem.ScoreAdd((int)_phaseTimeTotal);
            _PhaseText.text = "WAVE2";
            enemyPhase = EnemyPhase.phaseWait;
            _phaseJudge = false;
            PhaseReset();
        }
        if (enemyPhase == EnemyPhase.phase1 && _enemyPhaseNumber == 2)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase3();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        if (enemyPhase == EnemyPhase.phase1 && _enemyPhaseNumber == 3)
        {
            _enemyPos2 = _enemyPosPuls;
            EnemyPhase2();
            _enemyPhaseNumber = _enemyPhaseNumber - 1;
        }
        #endregion

        //_enem.ScoreAdd((int)_phaseTimeTotal);
    }

    private void PhaseReset()
    {
        if (!_phaseJudge)
        {
            if (enemyPhase == EnemyPhase.phase1)
            {
                //���t�F�[�Y�̓G�𐶐�
                EnemyPhase1();
                _enemyPhaseNumber = 3;
                _phaseJudge = true;
            }
            if (enemyPhase == EnemyPhase.phase2)
            {
                EnemyPhase4();
                _enemyPhaseNumber = 5;
                _phaseJudge = true;
            }
            if (enemyPhase == EnemyPhase.phase3)
            {
                EnemyPhase9();
                _enemyPhaseNumber = 6;
                _phaseJudge = true;
            }
            if (enemyPhase == EnemyPhase.phase4)
            {
                EnemyPhaseEND();
                _enemyPhaseNumber = 1;
                _phaseJudge = true;
            }
        }
    }

    #region"EnemyPhase�܂Ƃ�"
    #region"�t�F�[�Y1"
    private void EnemyPhase1()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[0]; _enemyPos++) {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase1.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }

    private void EnemyPhase2() {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[1] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase2.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }

    private void EnemyPhase3()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[2] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase3.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    #endregion
    #region"�t�F�[�Y2"
    private void EnemyPhase4()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[3] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase4.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    private void EnemyPhase5()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[4] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase5.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    private void EnemyPhase6()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[5] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase6.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    private void EnemyPhase7()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[6] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase7.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    private void EnemyPhase8()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[7] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase8.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    #endregion
    #region"�t�F�[�Y3"
    private void EnemyPhase9()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[8] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase9.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    private void EnemyPhase10()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[9] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase10.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    private void EnemyPhase11()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[10] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase11.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    private void EnemyPhase12()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[11] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase12.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }

    }
    private void EnemyPhase13()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[12] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase13.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    private void EnemyPhase14()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[13] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhase14.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }
    #endregion

    private void EnemyPhaseEND()
    {
        for (_enemyPos = _enemyPos2; _enemyPos < _enemysNumber[14] + _enemyPos2; _enemyPos++)
        {
            int _random = Random.Range(1, 9);
            GameObject _enemyPrefab = _poolPhaseEND.ReturnObjEnemy();
            _enemyPrefab.transform.position = _position[_random].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
            _enemyPosPuls = _enemyPosPuls + 1;
        }
    }

    #endregion

    public void Removed(GameObject obj){
        _enemyList.Remove(obj);
    }

    private void PoolSet()
    {
        _poolPhase1.Pool(_parent[0], _enemys[0], _enemysNumber[0]);
        _poolPhase2.Pool(_parent[1], _enemys[1], _enemysNumber[1]);
        _poolPhase3.Pool(_parent[2], _enemys[2], _enemysNumber[2]);

        _poolPhase4.Pool(_parent[3], _enemys[3], _enemysNumber[3]);
        _poolPhase5.Pool(_parent[4], _enemys[4], _enemysNumber[4]);
        _poolPhase6.Pool(_parent[5], _enemys[5], _enemysNumber[5]);
        _poolPhase7.Pool(_parent[6], _enemys[6], _enemysNumber[6]);
        _poolPhase8.Pool(_parent[7], _enemys[7], _enemysNumber[7]);

        _poolPhase9.Pool(_parent[8], _enemys[8], _enemysNumber[8]);
        _poolPhase10.Pool(_parent[9], _enemys[9], _enemysNumber[9]);
        _poolPhase11.Pool(_parent[10], _enemys[10], _enemysNumber[10]);
        _poolPhase12.Pool(_parent[11], _enemys[11], _enemysNumber[11]);
        _poolPhase13.Pool(_parent[12], _enemys[12], _enemysNumber[12]);
        _poolPhase14.Pool(_parent[13], _enemys[13], _enemysNumber[13]);

        _poolPhaseEND.Pool(_parent[14], _enemys[14], _enemysNumber[14]);
    }

    private void Parent() {

        _parent = new GameObject[15];

        for (int x = 0; x < 15;x++)
        {
            _parent[x] = _parentBase.transform.GetChild(x).gameObject;
        }
    }

    public void GameState(int _value)
    {
        _gameState = _value;
    }


}
