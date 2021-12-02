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
    /// ’e‚ÌˆÚ“®•ûŒü‚ğ•ÏX‚·‚éˆ—
    /// </summary>
    /// <param name="_newDir">V‚µ‚¢ˆÚ“®•ûŒü</param>
    public void ChangeMoveDirection(GameObject obj)
    {
        this.transform.rotation = obj.transform.rotation;
        _rb = GetComponent<Rigidbody>();
        //_moveDir = transform.forward;
        //e‚Æ“¯‚¶•ûŒü‚ğŒü‚©‚¹‚é

        _rb.AddForce(transform.forward * _moveSpeed);
    }
}
