using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private EnemControl _enem = new EnemControl();

    private Rigidbody _rb;

    [SerializeField]
    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //プレイヤーの発射された弾に当たった判定
        if (collision.gameObject.tag == "Bullet")
        {
            _enem.Removed(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    private void Move() {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, speed);
    }
}
