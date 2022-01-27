using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyEnemy : MonoBehaviour{

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
    [SerializeField]
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
    private ScoreManager _scoreManage;
    private int _enemyScore;
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
        _enemyhp = 1;
        this.GetComponent<EnemyManager>().EnemyHp(_enemyhp);
        _scoreManage = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        _enemyScore = _scoreManage.Fly(_enemyScore);
        this.GetComponent<EnemyManager>().EnemyScore(_enemyScore);
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
        float dis = Vector3.Distance(this.transform.position,_player.transform.position);
        if (dis > 8.0f && !_enemyMove)
        {
            _myAgent.destination = this.gameObject.transform.position;
        }
        if (dis < 8.0f && !_enemyMove)
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
            _box.SetActive(true);
            _ani.SetBool("atk", true);
            _atk = true;
            yield return new WaitForSeconds(1.5f);
            //Instantiate(_enemybul, transform.position, transform.rotation);
            _ani.SetBool("atk", false);
            _box.SetActive(false);
            _atk = false;
        }
    }
}
