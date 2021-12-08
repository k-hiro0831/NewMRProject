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

    private Rigidbody _rb;

    [SerializeField]
    private float speed;
    /// <summary>
    /// ìGÇÃêiÇﬁîªíË
    /// </summary>
    private bool _robot;
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
        _rdm = Random.Range(3,8);
        StartCoroutine("RobotAtk");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_robot && !_atk) {
            Move();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ÉvÉåÉCÉÑÅ[ÇÃî≠éÀÇ≥ÇÍÇΩíeÇ…ìñÇΩÇ¡ÇΩîªíË
        if (collision.gameObject.tag == "Bullet" && !_robot)
        {
            _robot = true;
            StartCoroutine("RobotDes");
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, speed);
    }

    private IEnumerator RobotAtk()
    {
        if (_robot)
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

    private IEnumerator RobotDes(){
        _ani.SetBool("dei", true);
        yield return new WaitForSeconds(2.0f);
        _enem.Removed(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
