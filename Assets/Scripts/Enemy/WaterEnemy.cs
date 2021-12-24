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
    private EnemyDestroy _enemyDes;

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
            _atk = true;
            yield return new WaitForSeconds(5.0f);
            _ani.SetBool("atk", false);
            _atk = false;
        }
    }
}
