using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGumBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float _moveSpeed = 100f;
    [SerializeField]
    private float _survivalTime = 1f;

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
    /// �e�̈ړ�������ύX���鏈��
    /// </summary>
    /// <param name="_newDir">�V�����ړ�����</param>
    public void ChangeMoveDirection(GameObject obj)
    {
        this.transform.rotation = obj.transform.rotation;
        //_rb = GetComponent<Rigidbody>();
        //_moveDir = transform.forward;
        //�e�Ɠ�����������������

        _rb.AddForce(transform.forward * _moveSpeed);
    }

    private IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(_survivalTime);
        this.gameObject.SetActive(false);

        yield break;
    }
}
