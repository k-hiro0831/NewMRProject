using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _ani;
    private bool _enemyMove;
    public bool _enemyMovePB
    {
        get { return _enemyMove; }
    }
    private EnemControl _enem = new EnemControl();
    // Start is called before the first frame update
    void Start()
    {
        _ani = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyDes()
    {
        _rb.isKinematic = true;
        _enemyMove = true;
        StartCoroutine("RobotDes");
    }

    private IEnumerator RobotDes()
    {
        _ani.SetBool("death", true);
        yield return new WaitForSeconds(2.5f);
        _enem.Removed(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
