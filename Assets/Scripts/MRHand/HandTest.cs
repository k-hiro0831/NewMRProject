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

    //TODO:数値が決まったら後でシリアライズフィールド消す
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
    //    Vector3 none_0 = joint[TrackedHandJoint.None].Position;                  // 対応なし
    //    Vector3 wrist_0 = joint[TrackedHandJoint.Wrist].Position;                // 手首
    //    Vector3 palm_0 = joint[TrackedHandJoint.Palm].Position;
    //}

    private void HandFind()
    {
        //手を認識している状態
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose pose))
        {
            IMixedRealityHandJointService handtrackingservice = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
            if (handtrackingservice != null)
            {
                //オブジェクトのトランスフォームとローテーションをハンドに追従させる
                //トランスフォーム変更
                TestObj.transform.position = handtrackingservice.RequestJointTransform(TrackedHandJoint.MiddleKnuckle, Handedness.Right).position;
                //ローテート取得、変更
                _gunRoteVector.x = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).rotation.x;
                _gunRoteVector.y = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).rotation.y;
                _gunRoteVector.z = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).rotation.z + _gunRoteOfset;
                TestObj.transform.rotation = Quaternion.Euler(_gunRoteVector);
                print("X:" + _gunRoteVector.x + "Y:" + _gunRoteVector.y + "Z:" + _gunRoteVector.z);
                
            }
        }
        //手を認識していない
        else
        {
            Debug.Log("deleta");
        }
    }


    private bool handgestureTest()
    {
        // 右手のみ判定したいので右手を取得
        var jointedHand = HandJointUtils.FindHand(Handedness.Right);
        // 手のひらが認識できているか
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm, out MixedRealityPose palmPose))
        {
            //指の先端オブジェクト
            MixedRealityPose thumbTipPose, indexTipPose, middleTipPose, ringTipPose, pinkyTipPose;
            //人差し指、小指を取得
            if (jointedHand.TryGetJoint(TrackedHandJoint.IndexTip, out indexTipPose) && jointedHand.TryGetJoint(TrackedHandJoint.PinkyTip, out pinkyTipPose))
            {
                //手のひら-人差し指の先端ベクトル、小指-人差し指の外積
                var handNormal = Vector3.Cross(indexTipPose.Position - palmPose.Position, pinkyTipPose.Position - indexTipPose.Position).normalized;
                handNormal *= (jointedHand.ControllerHandedness == Handedness.Right) ? 1.0f : -1.0f;
                if (Vector3.Angle(palmPose.Up, handNormal) < flatHandThreshold)
                {
                    return false;
                }
            }
            // 中指と薬指の情報を取得
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
        //// 手のひらを向けているか確認
        //return Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) < facingThreshold;
        return true;
    }
    
}

