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

    //TODO:数値が決まったら後でシリアライズフィールド消す
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
        //positionofsetを保存
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
    /// 手、ハンドジェスチャーを認識する関数
    /// </summary>
    private void HandFind()
    {
        // 右手のみ判定したいので右手を取得
        var jointedHand = HandJointUtils.FindHand(Handedness.Right);
        //手を認識している状態
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

                    //オブジェクトのトランスフォームとローテーションをハンドに追従させる
                    //トランスフォーム変更
                    //_testObj.transform.position = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).position + _objectPositionOfset;


                    //ローテート取得、変更
                    //_testObj.transform.rotation = handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).rotation;
                    //_testObj.transform.LookAt(handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right),Vector3.down);
                    //handtrackingservice.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right).parent = _testObj.transform;
                }
                

                //// 手のひらを向けているか確認
                //return Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) < facingThreshold;
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
        return Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) < facingThreshold;
        
    }
    
}

