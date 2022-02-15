using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour{

    #region"variable"
    private Rigidbody _rb;
    private Animator _ani;
    private bool _enemyMove;
    public bool _enemyMovePB
    {
        get { return _enemyMove; }
    }
    private NewEnemyControl _newEnem;
    private int _enemyHp;
    private EnemyValueManager _scoreManage;
    private int _enemyScore;
    private MoneyManager _money;
    private int _enemyMoney;
    private bool x = false;
    #endregion

    void Start()
    {
        _ani = GetComponent<Animator>();
        if (_ani == null)
        {
            _ani = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        }
        _rb = GetComponent<Rigidbody>();
        _newEnem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<NewEnemyControl>();
        _scoreManage = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<EnemyValueManager>();
        _money = GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<MoneyManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _enemyHp = 0;
        }

        if (_enemyHp == 0 || _enemyHp < 0)
        {
            if (!x)
            {
                EnemyDes();
                bool x = true;
            }  
        }
    }

    public void EnemyHp(int _value)
    {
        _enemyHp = _value;
    }

    public void EnemyScore(int _value)
    {
        _enemyScore = _value;
    }

    public void EnemyHpMinus(int _playerAtk)
    {
        _enemyHp = this.GetComponent<EnemyHpGauge>().MinusEnemyHp(_enemyHp, _playerAtk);
    }

    public void EnemyMoney(int _value)
    {
        _enemyMoney = _value;
    }

    public void EnemyDes()
    {
        _rb.isKinematic = true;
        _enemyMove = true;
        StartCoroutine("RobotDes");
    }

    public void EnemyRemove()
    {
        _rb.isKinematic = true;
        _enemyMove = true;
        StartCoroutine("RobotRemove");
    }

    private IEnumerator RobotDes()
    {
        _ani.SetBool("death", true);
        yield return new WaitForSeconds(3.0f);
        _scoreManage.ScoreAdd(_enemyScore);
        _money.MoneyPlus(_enemyMoney);
        //_enem.Removed(this.gameObject);
        //_newEnem.EnemyValueMinus(1);
        _newEnem.EnemyWaveCount(1);
        this.gameObject.SetActive(false);
        yield break;
    }

    private IEnumerator RobotRemove()
    {
        yield return new WaitForSeconds(3.0f);
        //_enem.Removed(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
