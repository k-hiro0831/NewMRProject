using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;

public class HandTest : MonoBehaviour
{
    [SerializeField]
    private GameObject TestObj = default;

    private Vector3 _gunRoteVector = default;

    [SerializeField, Range(0.0f, 90.0f)] private float flatHandThreshold = 45.0f;
    [SerializeField, Range(0.0f, 90.0f)] private float facingThreshold = 80.0f;

    //TODO:���l�����܂������ŃV���A���C�Y�t�B�[���h����
    [SerializeField]
    private float _gunRoteOfset = default;

    void Start()
    {
        
    }

    
    void Update()
    {
        HandFind();
        if (handgestureTest())
        {
            TestObj.SetActive(true);
        }
        else
        {
            TestObj.SetActive(false);
        }
    }

    //private HandStatus CheckCurrentHandStatus(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    //{
    //    IDictionary<TrackedHandJoint, MixedRealityPose> joint = eventData.InputData;
    //    Vector3 none_0 = joint[TrackedHandJoint.None].Position;                  // �Ή��Ȃ�
    //    Vector3 wrist_0 = joint[TrackedHandJoint.Wrist].Position;                // ���
    //    Vector3 palm_0 = joint[TrackedHandJoint.Palm].Position;
    //}

    private void HandFind()
    {
        //���F�����Ă�����
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose pose))
        {
            IMixedRealityHandJointService handtrackingservice = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
            if (handtrackingservice != null)
            {
                //�I�u�W�F�N�g�̃g�����X�t�H�[���ƃ��[�e�[�V�������n���h�ɒǏ]������
                //�g�����X�t�H�[���ύX
                TestObj.transform.position = handtrackingservice.RequestJointTransform(TrackedHandJoint.MiddleKnuckle, Handedness.Right).position;
                //���[�e�[�g�擾�A�ύX
                _gunRoteVector.x = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).rotation.x;
                _gunRoteVector.y = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).rotation.y;
                _gunRoteVector.z = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).rotation.z + _gunRoteOfset;
                TestObj.transform.rotation = Quaternion.Euler(_gunRoteVector);
                print("X:" + _gunRoteVector.x + "Y:" + _gunRoteVector.y + "Z:" + _gunRoteVector.z);
                
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
        //return Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) < facingThreshold;
        return true;
    }
    
}

