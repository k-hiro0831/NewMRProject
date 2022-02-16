using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterEnemy : MonoBehaviour
{
    #region"variable"
    [SerializeField]
    private GameObject _player;

    private NavMeshAgent _myAgent;

    private EnemControl _enem = new EnemControl();
    [SerializeField]
    private EnemyManager _enemyDes;

    [SerializeField]
    private GameObject _child;

    private Animator _ani;

    private Rigidbody _rb;

    [SerializeField]
    private float speed;
    /// <summary>
    /// ìGÇÃêiÇﬁîªíË
    /// </summary>
    private bool _enemyMove;
    /// <summary>
    /// ìGÇÃçUåÇîªíË
    /// </summary>
    private bool _atk;
    /// <summary>
    /// ìGÇÃçUåÇä‘äu
    /// </summary>
    private float _rdm;
    private int _enemyhp;
    private EnemyValueManager _scoreManage;
    private int _enemyScore;
    private int _enemyMoney;
    private int _enemyAtk;
    private bool _interval;
    [SerializeField]
    private GameObject[] _WarpArea;
    [SerializeField]
    private Vector3 _posAtk;
    [SerializeField]
    private ParticleSystem _atkEff;
    bool _atkBool;
    private Player _playerSc;
    [SerializeField]
    private bool _move;
    #endregion

    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("MainCamera");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
        _ani = _child.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rdm = Random.Range(6, 10);
        _enemyhp = Random.Range(4, 7);
        this.GetComponent<EnemyManager>().EnemyHp(_enemyhp);
        _scoreManage = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<EnemyValueManager>();
        _enemyScore = _scoreManage.Water(_enemyScore);
        this.GetComponent<EnemyManager>().EnemyScore(_enemyScore);
        _enemyMoney = _scoreManage.WaterMoney(_enemyMoney);
        this.GetComponent<EnemyManager>().EnemyMoney(_enemyMoney);
        GameObject Area = GameObject.FindGameObjectWithTag("WarpArea");
        _playerSc = _player.GetComponent<Player>();

        for (int i = 0; i < 3; i++)
        {
            _WarpArea[i] = Area.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        Qua();

        _enemyMove = _enemyDes._enemyMovePB;

        float dis = Vector3.Distance(_player.transform.position, this.transform.position);

        if (_atk)
        {
            AtkEff();
        }

        if (dis < 5.0f)
        {
            _interval = true;
        }
        if (dis > 5.0f)
        {
            _interval = false;
        }

        if (!_enemyMove && !_interval && !_atk && _move)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _player.transform.position, speed * 0.1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Move" && !_move)
        {
            StartCoroutine("Atk");
            _move = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Move" && _move)
        {
            StopCoroutine("Atk");
            _move = false;
        }
    }
    private IEnumerator Atk()
    {
        if (_enemyMove)
        {
            yield break;
        }
        while (true)
        {
            yield return new WaitForSeconds(_rdm);
            _ani.SetBool("atk", true);
            _atk = true;
            _atkEff.transform.position = this.transform.position;
            _posAtk = _player.transform.position;
            _atkEff.Play();
            yield return new WaitForSeconds(5.0f);
            _ani.SetBool("atk", false);
            _atk = false;
        }
    }

    private void AtkEff()
    {
        _atkEff.transform.position = Vector3.MoveTowards(_atkEff.transform.position, _posAtk, speed * 5f);
        float disA = Vector3.Distance(_posAtk, _atkEff.transform.position);
        float disB = Vector3.Distance(_player.transform.position, _posAtk);

        if (disA < 0.9f && disB < 0.9f && !_atkBool)
        {
            _atkBool = true;
            _playerSc.Atk(7);
            Invoke("AtkBoolReset", 2.0f);
        }

    }

    private void AtkBoolReset()
    {
        _atkBool = false;
    }

    private void Qua()
    {
        Vector3 vector3 = _player.transform.position - this.transform.position;
        vector3.y = 0f;

        Quaternion quaternion = Quaternion.LookRotation(vector3);
        this.transform.rotation = quaternion;
    }
}
