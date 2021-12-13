using UnityEngine;

public class ShotRay : MonoBehaviour
{
    private RaycastHit _hit;
    private bool _isHit = false;
    private GameObject _hitObj = null;

    [SerializeField]
    private float _rayLength = 10f;
    [SerializeField]
    private GameObject _rayObj = null;

    private Vector3 _thisPos = Vector3.zero;

    //���s�������ʂ���t���O
    private bool _isPlaying = false;

    private void Start()
    {
        _isPlaying = true;
        _isHit = false;
    }

    private void Update()
    {
        _thisPos = _rayObj.transform.position;

        //�G�ɂԂ����Ă����True�A�����łȂ��Ȃ�False
        if (Physics.Raycast(_thisPos, _rayObj.transform.forward, out _hit, _rayLength, LayerMask.GetMask("Enemy")))
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

    /// <summary>
    /// �q�b�g����t���O��Ԃ�
    /// </summary>
    /// <returns>�q�b�g�t���O</returns>
    public bool ReturnIsHit()
    {
        return _isHit;
    }
    /// <summary>
    /// �q�b�g����Ԃ�
    /// </summary>
    /// <returns></returns>
    public RaycastHit ReturnRayCastHit()
    {
        return _hit;
    }


    private void OnDrawGizmos()
    {
        //���s���łȂ��Ƃ��͏�������
        //�����̏d��������邽�߂̏���
        if (!_isPlaying)
        {
            _thisPos = _rayObj.transform.position;
            _isHit = Physics.Raycast(_thisPos, _rayObj.transform.forward, out _hit,_rayLength);
        }

        //���C���������Ă���ԁA�������Ă���ʒu�Ƀ{�b�N�X��\��
        if (_isHit)
        {
            //�����Փˈʒu�܂ŕ\��
            Gizmos.DrawRay(_thisPos, _rayObj.transform.forward * _hit.distance);
        }
        else
        {
            //�����w�肵�������\��
            Gizmos.DrawRay(_thisPos, _rayObj.transform.forward * _rayLength);
        }
    }
}
