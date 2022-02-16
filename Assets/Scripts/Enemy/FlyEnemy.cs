using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyEnemy : MonoBehaviour{

    #region"variable"
    [SerializeField]
    private GameObject _player;

    private Transform _playerT;

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
    private EnemyValueManager _scoreManage;
    private int _enemyScore;
    private int _enemyMoney;
    private int _enemyAtk;
    [SerializeField]
    private bool _interval;
    private EnemyAtkMethod _enemyAtkM;
    private bool _move;
    #endregion

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("MainCamera");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
        _ani = _child.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rdm = Random.Range(3, 8);
        _box.SetActive(false);
        _enemyhp = 1;
        this.GetComponent<EnemyManager>().EnemyHp(_enemyhp);
        _scoreManage = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<EnemyValueManager>();
        _enemyScore = _scoreManage.Fly(_enemyScore);
        this.GetComponent<EnemyManager>().EnemyScore(_enemyScore);

        _enemyMoney = _scoreManage.FlyMoney(_enemyMoney);
        this.GetComponent<EnemyManager>().EnemyMoney(_enemyMoney);

        _enemyAtk = _scoreManage.FlyAtk(_enemyAtk);
        //this.GetComponent<EnemyManager>().EnemyFlyAtk(_enemyAtk);
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

        if (!_enemyMove && !_interval && !_atk && _move)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _player.transform.position, speed * 0.1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Move" && !_move)
        {
            StartCoroutine("Atk");
            _move = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Move" && _move)
        {
            StopCoroutine("Atk");
            _move = false;
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
