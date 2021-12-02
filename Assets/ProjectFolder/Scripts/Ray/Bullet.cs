using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]
    private float _moveSpeed = 100f;

    private Vector3 _moveDir = Vector3.zero;

    private void Start()
    {
        //_rb = GetComponent<Rigidbody>();
        //_moveDir = transform.forward;
        //_rb.AddForce(_moveDir * _moveSpeed);
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// 弾の移動方向を変更する処理
    /// </summary>
    /// <param name="_newDir">新しい移動方向</param>
    public void ChangeMoveDirection(GameObject obj)
    {
        this.transform.rotation = obj.transform.rotation;
        _rb = GetComponent<Rigidbody>();
        //_moveDir = transform.forward;
        //銃と同じ方向を向かせる

        _rb.AddForce(transform.forward * _moveSpeed);
    }
}
