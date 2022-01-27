using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;

public class HandTest : MonoBehaviour
{
    [SerializeField]
    private GameObject _testObj = default;

    private Vector3 _gunRoteVector = default;

    [SerializeField, Range(0.0f, 90.0f)] 
    private float flatHandThreshold = 45.0f;
    [SerializeField, Range(0.0f, 90.0f)] 
    private float facingThreshold = 80.0f;

    //TODO:���l�����܂������ŃV���A���C�Y�t�B�[���h����
    [SerializeField,Range(0.0f,0.1f)]
    private float _objectPositionOfsetX = default;
    [SerializeField, Range(-0.1f, 0.1f)]
    private float _objectPositionOfsetY = default;
    [SerializeField, Range(0.0f, 0.1f)]
    private float _objectPositionOfsetZ = default;

    private Vector3 _objectPositionOfset = default;

    [SerializeField,Range(0.0f,90.0f)]
    private float _objectRotateOfsetX = default;

    private Vector3 _objectRotateOfset = default;

    private void Start()
    {
        //positionofset��ۑ�
        _objectPositionOfset.x = _objectPositionOfsetX;
        _objectPositionOfset.y = _objectPositionOfsetY;
        _objectPositionOfset.z = _objectPositionOfsetZ;

        _objectRotateOfset.x = _objectRotateOfsetX;

    }

    private void Update()
    {
        HandFind();
        
    }

   
    /// <summary>
    /// ��A�n���h�W�F�X�`���[��F������֐�
    /// </summary>
    private void HandFind()
    {
        // �E��̂ݔ��肵�����̂ŉE����擾
        var jointedHand = HandJointUtils.FindHand(Handedness.Right);
        //���F�����Ă�����
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose pose))
        {
            IMixedRealityHandJointService handtrackingservice = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
            if (handtrackingservice != null)
            {
                if(Vector3.Angle(pose.Up, CameraCache.Main.transform.forward) > facingThreshold)
                {
                    

                    MixedRealityPose wristPose, indextipPose;
                    if (jointedHand.TryGetJoint(TrackedHandJoint.Wrist, out wristPose)&&jointedHand.TryGetJoint(TrackedHandJoint.IndexTip,out indextipPose))
                    {
                        transform.RotateAround(wristPose.Position, indextipPose.Position - wristPose.Position, 360 / 0.2f * Time.deltaTime);
                        //print(wristPose.Up);
                        //_testObj.transform.position = wristPose.Position + _objectPositionOfset;
                    }

                    //�I�u�W�F�N�g�̃g�����X�t�H�[���ƃ��[�e�[�V�������n���h�ɒǏ]������
                    //�g�����X�t�H�[���ύX
                    //_testObj.transform.position = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).position + _objectPositionOfset;


                    //���[�e�[�g�擾�A�ύX
                    //_testObj.transform.rotation = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).rotation;
                    //_testObj.transform.LookAt(handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right),Vector3.down);
                    //handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).parent = _testObj.transform;
                }
                

                //// ��̂Ђ�������Ă��邩�m�F
                //return Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) < facingThreshold;
            }
        }
        //���F�����Ă��Ȃ�
        else
        {
            Debug.Log("deleta");
        }
    }



    private bool handgestureTest()
    {
        // �E��̂ݔ��肵�����̂ŉE����擾
        var jointedHand = HandJointUtils.FindHand(Handedness.Right);
        // ��̂Ђ炪�F���ł��Ă��邩
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm, out MixedRealityPose palmPose))
        {
            //�w�̐�[�I�u�W�F�N�g
            MixedRealityPose thumbTipPose, indexTipPose, middleTipPose, ringTipPose, pinkyTipPose;
            //�l�����w�A���w���擾
            if (jointedHand.TryGetJoint(TrackedHandJoint.IndexTip, out indexTipPose) && jointedHand.TryGetJoint(TrackedHandJoint.PinkyTip, out pinkyTipPose))
            {
                //��̂Ђ�-�l�����w�̐�[�x�N�g���A���w-�l�����w�̊O��
                var handNormal = Vector3.Cross(indexTipPose.Position - palmPose.Position, pinkyTipPose.Position - indexTipPose.Position).normalized;
                handNormal *= (jointedHand.ControllerHandedness == Handedness.Right) ? 1.0f : -1.0f;
                if (Vector3.Angle(palmPose.Up, handNormal) < flatHandThreshold)
                {
                    return false;
                }
            }
            // ���w�Ɩ�w�̏����擾
            if (jointedHand.TryGetJoint(TrackedHandJoint.MiddleTip, out middleTipPose) && jointedHand.TryGetJoint(TrackedHandJoint.RingTip, out ringTipPose))
            {
                var handNormal = Vector3.Cross(middleTipPose.Position - palmPose.Position, ringTipPose.Position - indexTipPose.Position).normalized;
                handNormal *= (jointedHand.ControllerHandedness == Handedness.Right) ? 1.0f : -1.0f;
                if (Vector3.Angle(palmPose.Up, handNormal) < flatHandThreshold)
                {
                    return false;
                }
            }
        }
        //// ��̂Ђ�������Ă��邩�m�F
        return Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) < facingThreshold;
        
    }
    
}

