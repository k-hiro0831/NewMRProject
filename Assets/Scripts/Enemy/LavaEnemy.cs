using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LavaEnemy : MonoBehaviour
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
    [SerializeField]
    private MouseTarget _mouse;
    #endregion

    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("MainCamera");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
        _ani = _child.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rdm = Random.Range(7, 10);
        StartCoroutine("Atk");
        _enemyhp = Random.Range(10, 15);
        this.GetComponent<EnemyManager>().EnemyHp(_enemyhp);
        _scoreManage = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<EnemyValueManager>();
        _enemyScore = _scoreManage.Lava(_enemyScore);
        this.GetComponent<EnemyManager>().EnemyScore(_enemyScore);
        _enemyMoney = _scoreManage.LavaMoney(_enemyMoney);
        this.GetComponent<EnemyManager>().EnemyMoney(_enemyMoney);
    }

    void Update()
    {
        _enemyMove = _enemyDes._enemyMovePB;
        this.transform.LookAt(_player.transform);
        if (_myAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            SetDestination();
        }
    }

    public void SetDestination()
    {
        if (!_enemyMove)
        {
            var endPoint = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
            _myAgent.destination = endPoint;
        }

        if (_enemyMove || _atk)
        {
            _myAgent.destination = this.gameObject.transform.position;
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
            _mouse.EnemyAtk();
            _atk = true;
            yield return new WaitForSeconds(5.0f);
            _ani.SetBool("atk", false);
            _atk = false;
        }
    }
}
