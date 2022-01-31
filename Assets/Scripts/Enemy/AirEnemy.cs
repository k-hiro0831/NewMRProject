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
    /// �G�̐i�ޔ���
    /// </summary>
    private bool _enemyMove;
    /// <summary>
    /// �G�̍U������
    /// </summary>
    private bool _atk;
    /// <summary>
    /// �G�̍U���Ԋu
    /// </summary>
    private float _rdm;
    /// <summary>
    /// �U���̓����蔻��
    /// </summary>
    [SerializeField]
    private GameObject _box;

    private int _enemyhp;
    private EnemyValueManager _scoreManage;
    private int _enemyScore;
    private int _enemyMoney;
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
            _box.SetActive(true);
            _ani.SetBool("atk", true);
            _atk = true;
            yield return new WaitForSeconds(1.2f);
            _ani.SetBool("atk", false);
            _box.SetActive(false);
            _atk = false;
        }
    }
}
