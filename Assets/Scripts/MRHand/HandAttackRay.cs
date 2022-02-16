using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;

//attackmanager�Ɠ����I�u�W�F�N�g�ɓ����
public class HandAttackRay : MonoBehaviour
{
    //TODO:�U��������Ƃ郌�C���肩��o���X�N���v�g
    //��񂩂�l�����w�̕����Ń��C���΂�
    [SerializeField, Range(0.0f, 90.0f)]
    private float _facingThreshold = 80.0f;

    private Vector3 _attackRayVector3 = default;

    [SerializeField,Range(0.0f,20.0f),Header("�U�����C�̒���")]
    private float _rayRange = default;

    [SerializeField,Range(0.0f,0.5f),Header("�o�����C�̔��a")]
    private float _radius = default;

    [SerializeField,Range(0.0f,5.0f),Header("�U���̃N�[���^�C��")]
    private float _attackCoolTime = default;

    [SerializeField, Header("�E�肩��Ray�I�u�W�F�N�g")]
    private GameObject _rayObjectRight = default;

    [SerializeField, Header("���肩��Ray�I�u�W�F�N�g")]
    private GameObject _rayObjectLeft = default;

    [SerializeField]
    private Color g_xrayColor = default;

    private float _attackCoolTimeCount = default;

    private RaycastHit _attackRayHit = default;

    private AttackManager _attackManager = default;

    private Vector3 _drawRayStartPosition = default;

    private bool _attackFlag = true;

    private const string TAG_NAME_ENEMY = "Enemy";

    private void Start()
    {
        _attackManager = GetComponent<AttackManager>();
    }

    
    private void Update()
    {
        AttackRay();
    }

    /// <summary>
    /// �肪�ǂ����������Ă��邩�`�F�b�N����֐�
    /// </summary>
    private bool HandDirectionCheck()
    {
        bool check = false;
        // �E��̂ݔ��肵�����̂ŉE����擾
        var jointedHand = HandJointUtils.FindHand(Handedness.Right);
        //��̂Ђ���擾�ł����Ƃ��ɑ���
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm, out MixedRealityPose palmPose))
        {
            //��̂Ђ炪�ǂ����������Ă��邩�`�F�b�N
            if (Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) < _facingThreshold)
            {
                check = true;
            }
        }
        //��̂Ђ��F���ł��Ă��Ȃ����ɑ���
        else
        {

        }

        return check;
    }

    /// <summary>
    /// �A�^�b�N���C���o���֐�
    /// </summary>
    private void AttackRay()
    {
        AttackFlagSystem();
        
        // ��̂Ђ炪�F���ł��Ă��邩
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm,Handedness.Right, out MixedRealityPose palmPose))
        {
            
            //�w�̐�[�I�u�W�F�N�g
            MixedRealityPose indexKnucklePose, wristPose;
            //���Ɛl�����w�̍��{���擾
            if(HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, Handedness.Right, out indexKnucklePose) && HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist,Handedness.Right , out wristPose))
            {
                //��񂩂�l�����w�̍��{�Ɍ����Ẵx�N�g�����v�Z
                _attackRayVector3 = (indexKnucklePose.Position - wristPose.Position).normalized;
                _drawRayStartPosition = wristPose.Position;
                //���C���΂��ē������Ă��邩����A�G�ɓ������Ă����ꍇ�ɑ���
                if (Physics.SphereCast(_drawRayStartPosition, _radius,_attackRayVector3,out _attackRayHit,_rayRange) && 
                    _attackRayHit.collider.gameObject.transform.tag== TAG_NAME_ENEMY)
                {
                    RayHitAttack(_attackRayHit.collider.gameObject);
                }


                _rayObjectRight.SetActive(true);
                //�I�u�W�F�N�g�ʒu��ύX
                _rayObjectRight.transform.position = wristPose.Position;
                //�I�u�W�F�N�g�̌�����ύX
                _rayObjectRight.transform.LookAt(indexKnucklePose.Position);
            }

        }
        else
        {
            _rayObjectRight.SetActive(false);
        }

        
        // ��̂Ђ炪�F���ł��Ă��邩
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out MixedRealityPose palmPoseLeft))
        {
            
            //�w�̐�[�I�u�W�F�N�g
            MixedRealityPose indexKnucklePoseLeft, wristPoseLeft;
            //���Ɛl�����w�̍��{���擾
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, Handedness.Left,out indexKnucklePoseLeft) && HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Left,out wristPoseLeft))
            {
                //��񂩂�l�����w�̍��{�Ɍ����Ẵx�N�g�����v�Z
                _attackRayVector3 = (indexKnucklePoseLeft.Position - wristPoseLeft.Position).normalized;
                _drawRayStartPosition = wristPoseLeft.Position;
                //���C���΂��ē������Ă��邩����A�G�ɓ������Ă����ꍇ�ɑ���
                if (Physics.SphereCast(_drawRayStartPosition, _radius, _attackRayVector3, out _attackRayHit, _rayRange) &&
                    _attackRayHit.collider.gameObject.transform.tag == TAG_NAME_ENEMY)
                {
                    RayHitAttack(_attackRayHit.collider.gameObject);
                }

                //����U��ray����
                _rayObjectLeft.SetActive(true);
                //ray�I�u�W�F�N�g�̈ʒu�ύX
                _rayObjectLeft.transform.position = wristPoseLeft.Position;
                //ray�I�u�W�F�N�g�̌����ύX
                _rayObjectLeft.transform.LookAt(indexKnucklePoseLeft.Position);

            }
        }
        else
        {
            _rayObjectLeft.SetActive(false);
        }
    }

    /// <summary>
    /// �U���������Ă�
    /// </summary>
    /// <param name="enemy">�U������Ώۂ�����</param>
    private void RayHitAttack(GameObject enemy)
    {
        if (_attackFlag)
        {
            _attackManager.AttackEvent(enemy);
            _attackFlag = false;
        }
    }

    /// <summary>
    /// attackFlag��false�̎��ɃJ�E���g���񂵂�true�ɂ���
    /// </summary>
    private void AttackFlagSystem()
    {
        if (!_attackFlag)
        {
            _attackCoolTimeCount += Time.deltaTime;
            if (_attackCoolTimeCount > _attackCoolTime)
            {
                _attackFlag = true;
                _attackCoolTimeCount = 0;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if (_attackFlag)
        {
            Gizmos.color = g_xrayColor;
            Gizmos.DrawRay(_drawRayStartPosition, _attackRayVector3 * _rayRange);
            Gizmos.DrawWireSphere(_attackRayHit.point, _radius);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position + transform.forward * _rayRange, _radius);
        }


    }
}
