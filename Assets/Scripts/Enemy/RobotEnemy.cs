using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _enemybul;

    private Vector3 _bulletTrf = new Vector3(0.0f, 3.0f, 3.0f);

    private EnemControl _enem = new EnemControl();
    [SerializeField]
    private EnemyDestroy _enemyDes;

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

    private Animator _ani;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
        _ani = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rdm = Random.Range(3,8);
        StartCoroutine("RobotAtk");
    }

    // Update is called once per frame
    void Update()
    {
        _enemyMove = _enemyDes._enemyMovePB;
        this.transform.LookAt(_player.transform);
        if (!_enemyMove && !_atk) {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, speed);
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
