using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour{

    #region"variable"
    private Rigidbody _rb;
    private Animator _ani;
    private bool _enemyMove;
    public bool _enemyMovePB
    {
        get { return _enemyMove; }
    }
    private EnemControl _enem = new EnemControl();
    #endregion

    void Start()
    {
        _ani = GetComponent<Animator>();
        if (_ani == null)
        {
            _ani = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        }
        _rb = GetComponent<Rigidbody>();
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnemyDes();
        }
    }

    public void EnemyDes()
    {
        _rb.isKinematic = true;
        _enemyMove = true;
        StartCoroutine("RobotDes");
    }

    public void EnemyRemove()
    {
        _rb.isKinematic = true;
        _enemyMove = true;
        StartCoroutine("RobotRemove");
    }

    private IEnumerator RobotDes()
    {
        _ani.SetBool("death", true);
        yield return new WaitForSeconds(3.0f);
        _enem.Removed(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private IEnumerator RobotRemove()
    {
        yield return new WaitForSeconds(3.0f);
        _enem.Removed(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
