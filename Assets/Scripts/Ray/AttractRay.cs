using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractRay : MonoBehaviour
{
    RaycastHit _hit;
    [SerializeField]
    private bool _isHit = false;
    [SerializeField]
    private GameObject _hitObj;

    [SerializeField]
    private float _rayLength = 10f;
    private Vector3 _thisPos = Vector3.zero;
    private float _thisScale = 1f;

    //���s�������ʂ���t���O
    private bool _isPlaying = false;

    private void Start()
    {
        _isPlaying = true;
        _isHit = false;
    }

    private void Update()
    {
        _thisPos = transform.position;

        //�G�ɂԂ����Ă����True�A�����łȂ��Ȃ�False
        if (Physics.Raycast(_thisPos, transform.forward, out _hit, _rayLength, LayerMask.GetMask("Item")))
        {
            _isHit = true;
            _hitObj = _hit.collider.gameObject.gameObject;
        }
        else
        {
            _isHit = false;
            _hitObj = null;
        }
    }

    public GameObject ReturnHitObj()
    {
        return _hitObj;
    }

    /// <summary>
    /// �q�b�g����t���O��Ԃ�
    /// </summary>
    /// <returns>�q�b�g�t���O</returns>
    public bool ReturnIsHit()
    {
        return _isHit;
    }

    private void OnDrawGizmos()
    {
        //���s���łȂ��Ƃ��͏�������
        //�����̏d��������邽�߂̏���
        if (!_isPlaying)
        {
            _thisPos = transform.position;
            _isHit = Physics.Raycast(_thisPos, transform.forward, out _hit, _rayLength);
        }

        //���C���������Ă���ԁA�������Ă���ʒu�Ƀ{�b�N�X��\��
        if (_isHit)
        {
            //�����Փˈʒu�܂ŕ\��
            Gizmos.DrawRay(_thisPos, transform.forward * _hit.distance);
        }
        else
        {
            //�����w�肵�������\��
            Gizmos.DrawRay(_thisPos, transform.forward * _rayLength);
        }
    }
}
