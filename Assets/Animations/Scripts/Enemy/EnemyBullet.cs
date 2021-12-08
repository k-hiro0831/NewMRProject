using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody _rb;
    private GameObject _player;
    private Vector3 _playerTrans;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
        //生成された時点でのプレイヤーの位置を保存
        _playerTrans = _player.transform.position;
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playerTrans, speed);
    }
}
