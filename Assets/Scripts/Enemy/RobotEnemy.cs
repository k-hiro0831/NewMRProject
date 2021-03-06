using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotEnemy : MonoBehaviour{

    #region"variable"
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _enemybul;

    private NavMeshAgent _myAgent;

    private Vector3 _bulletTrf = new Vector3(0.0f, 3.0f, 3.0f);

    private EnemControl _enem = new EnemControl();
    [SerializeField]
    private EnemyManager _enemyDes;

    private Rigidbody _rb;

    [SerializeField]
    private float speed;
    /// <summary>
    /// ?G?̐i?ޔ???
    /// </summary>
    private bool _enemyMove;
    /// <summary>
    /// ?G?̍U??????
    /// </summary>
    private bool _atk;
    /// <summary>
    /// ?G?̍U???Ԋu
    /// </summary>
    private float _rdm;

    private Animator _ani;
#endregion

    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
        _ani = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rdm = Random.Range(3,8);
        StartCoroutine("RobotAtk");
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
        if (!_enemyMove){
            var endPoint = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
            _myAgent.destination = endPoint;
        }

        if (_enemyMove || _atk){
            _myAgent.destination = this.gameObject.transform.position;
        }
    }

    private IEnumerator RobotAtk()
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
            yield return new WaitForSeconds(1);
            Instantiate(_enemybul, transform.position, transform.rotation);
            _ani.SetBool("atk", false);
            _atk = false;
        }
    }
}
