using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]
    private float _moveSpeed = 100f;
    [SerializeField]
    private float _survivalTime = 1f;

    private RaycastHit _hit;
    private Vector3 _thisPos = Vector3.zero;
    [SerializeField]
    private float _rayRadius = 10f;

    [SerializeField]
    private GameObject _bulletMarkPrefab = null;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        _thisPos = transform.position;

        if (Physics.SphereCast(_thisPos, _rayRadius, transform.forward, out _hit, 1, LayerMask.GetMask("Enemy")))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        //���C�̕\��
        _thisPos = transform.position;
        Gizmos.DrawWireSphere(_thisPos, _rayRadius);
    }

    /// <summary>
    /// �e�̈ړ�������ύX���鏈��
    /// </summary>
    /// <param name="_newDir">�V�����ړ�����</param>
    public void ChangeMoveDirection(GameObject obj)
    {
        this.transform.rotation = obj.transform.rotation;
        _rb.AddForce(transform.forward * _moveSpeed);
        StartCoroutine(EndTimer());
    }

    /// <summary>
    /// ��莞�Ԍo�ߌ�A��\��
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(_survivalTime);
        this.gameObject.SetActive(false);
        yield break;
    }
}
