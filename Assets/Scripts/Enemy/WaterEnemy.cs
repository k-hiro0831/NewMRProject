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
    private SFXControllerV3D _sfx;
    [SerializeField]
    private ProgressControlV3D _pro;

    [SerializeField]
    private MouseTargetV3D _mouse;
    #endregion

    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("MainCamera");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
        _ani = _child.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rdm = 5;
        StartCoroutine("Atk");
        _enemyhp = Random.Range(4, 7);
        this.GetComponent<EnemyManager>().EnemyHp(_enemyhp);
        _scoreManage = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<EnemyValueManager>();
        _enemyScore = _scoreManage.Water(_enemyScore);
        this.GetComponent<EnemyManager>().EnemyScore(_enemyScore);
        _enemyMoney = _scoreManage.WaterMoney(_enemyMoney);
        this.GetComponent<EnemyManager>().EnemyMoney(_enemyMoney);
        GameObject Area = GameObject.FindGameObjectWithTag("WarpArea");

        for(int i = 0; i < 3; i++)
        {
            _WarpArea[i] = Area.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        //this.transform.LookAt(_player.transform);
        Qua();

        _enemyMove = _enemyDes._enemyMovePB;

        float dis = Vector3.Distance(_player.transform.position, this.transform.position);

        if (dis < 1.5f)
        {
            _interval = true;
        }
        if (dis > 1.5f)
        {
            _interval = false;
        }

        if (!_enemyMove && !_interval && !_atk)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _player.transform.position, speed * 0.1f);
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
            _mouse.Atk(_atk);
            Invoke("AtkEff", 0.7f);
            yield return new WaitForSeconds(5.0f);
            _ani.SetBool("atk", false);
            _atk = false;
            _mouse.Atk(_atk);
        }
    }

    private void AtkEff()
    {
        _sfx.EnemyAtk();
        _pro.EnemyAtk();
    }

    private void Qua()
    {
        Vector3 vector3 = _player.transform.position - this.transform.position;
        vector3.y = 0f;

        Quaternion quaternion = Quaternion.LookRotation(vector3);
        this.transform.rotation = quaternion;
    }
}
