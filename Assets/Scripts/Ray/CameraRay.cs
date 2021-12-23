using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    private RaycastHit _cameraRay = default;

    [SerializeField]
    private float _rayLength = 10f;

    private GameObject _cameraObj = default;

    [SerializeField]
    private bool _cameraRayHit = false;

    private GameObject _enemyObject = default;

    private bool _playFlag = false;

    [SerializeField,Range(0.0f,1.0f)]
    private float _radius = 1.0f;

    [SerializeField]
    private Color g_xrayColor = default;

    private const string TAG_ENEMY = "Enemy";

    private void Start()
    {
        _playFlag = true;
        _cameraObj = this.gameObject;
    }

    
    private void Update()
    {
        
        //���C���������Ă�Ƃ��ɑ���
        if(Physics.SphereCast(_cameraObj.transform.position, _radius, _cameraObj.transform.forward, out _cameraRay, _rayLength) &&
            _cameraRay.collider.gameObject.gameObject.tag == TAG_ENEMY)
        {
            
            //�������Ă���t���O�𗧂ĂăI�u�W�F�N�g�擾
            
            _enemyObject = _cameraRay.collider.gameObject.gameObject;
            _cameraRayHit = true;
            //if (_enemyObject.tag == "Enemy")
            //{
                
            //}
            
        }
        else
        {
            //�������Ă���t���O��false�ɂ��ăI�u�W�F�N�g������
            _cameraRayHit = false;
            _enemyObject = null;
        }
    }

    /// <summary>
    /// ray���������Ă��邩�𔻒�
    /// </summary>
    /// <returns>true,false</returns>
    public bool RayHit()
    {
        return _cameraRayHit;
    }

    /// <summary>
    /// �������Ă���G�l�~�[�I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetHitEnemyObj()
    {
        return _enemyObject;
    }

    private void OnDrawGizmos()
    {
        if (_cameraRayHit)
        {
            Gizmos.color = g_xrayColor;
            Gizmos.DrawRay(_cameraObj.transform.position, _cameraObj.transform.forward * _rayLength);
            Gizmos.DrawWireSphere(_cameraRay.point, _radius);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position + transform.forward * _rayLength, _radius);
        }
        
       
    }
}
