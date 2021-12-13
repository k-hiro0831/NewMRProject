using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private EnemControl _enem = new EnemControl();
    [SerializeField]
    private EnemyDestroy _enemyDes;

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


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
        _ani = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rdm = Random.Range(3, 8);
        StartCoroutine("Atk");
    }

    // Update is called once per frame
    void Update()
    {
        _enemyMove = _enemyDes._enemyMovePB;
        this.transform.LookAt(_player.transform);
        if (!_enemyMove && !_atk)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, speed);
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
            yield return new WaitForSeconds(1.5f);
            //Instantiate(_enemybul, transform.position, transform.rotation);
            _ani.SetBool("atk", false);
            _atk = false;
        }
    }
}
