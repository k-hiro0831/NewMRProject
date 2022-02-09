using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AirEnemy : MonoBehaviour
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
    /// <summary>
    /// çUåÇÇÃìñÇΩÇËîªíË
    /// </summary>
    [SerializeField]
    private GameObject _box;

    private int _enemyhp;
    private EnemyValueManager _scoreManage;
    private int _enemyScore;
    private int _enemyMoney;
    private int _enemyAtk;
    private bool _interval;
    private EnemyAtkMethod _enemyAtkM;
    #endregion

    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("MainCamera");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
        _ani = _child.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rdm = Random.Range(3, 8);
        StartCoroutine("Atk");
        _box.SetActive(false);
        _enemyhp= 10;
        this.GetComponent<EnemyManager>().EnemyHp(_enemyhp);
        _scoreManage = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<EnemyValueManager>();
        _enemyScore = _scoreManage.Air(_enemyScore);
        this.GetComponent<EnemyManager>().EnemyScore(_enemyScore);
        _enemyMoney = _scoreManage.AirMoney(_enemyMoney);
        this.GetComponent<EnemyManager>().EnemyMoney(_enemyMoney);
        _enemyAtk = _scoreManage.AirAtk(_enemyAtk);
        _enemyAtkM = _box.GetComponent<EnemyAtkMethod>();
        _enemyAtkM.EnemyAtk(_enemyAtk);
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
            _box.SetActive(true);
            _ani.SetBool("atk", true);
            _atk = true;
            yield return new WaitForSeconds(1.2f);
            _ani.SetBool("atk", false);
            _box.SetActive(false);
            _atk = false;
        }
    }

    private void Qua()
    {
        Vector3 vector3 = _player.transform.position - this.transform.position;
        vector3.y = 0f;

        Quaternion quaternion = Quaternion.LookRotation(vector3);
        this.transform.rotation = quaternion;
    }
}
