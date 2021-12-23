using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;

public class VectorGizmo : MonoBehaviour
{

    [SerializeField]
    private Color g_xrayColor = default;
    [SerializeField]
    private Color g_yrayColor = default;
    [SerializeField]
    private Color g_zrayColor = default;

    [SerializeField]
    private string _handPosTag = default;
    [SerializeField]
    private float _length = default;
        IMixedRealityHand jointedHand = null;
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose pose))
        {
            // 右手のみ判定したいので右手を取得
            jointedHand = HandJointUtils.FindHand(Handedness.Right);
        }
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose posee))
        {
            // 右手のみ判定したいので右手を取得
            jointedHand = HandJointUtils.FindHand(Handedness.Left);
        }

    }

    private void OnDrawGizmos()
    {
        // 手のひらが認識できているか
        if (jointedHand != null && jointedHand.TryGetJoint(TrackedHandJoint.Palm, out MixedRealityPose palmPose))
        {

            Gizmos.color = g_yrayColor;
            Gizmos.DrawRay(palmPose.Position, palmPose.Up * _length);
            Gizmos.color = g_zrayColor;
            Gizmos.DrawRay(palmPose.Position, palmPose.Forward * _length);
            Gizmos.color = g_xrayColor;
            Gizmos.DrawRay(palmPose.Position, palmPose.Right * _length);
            if (jointedHand.TryGetJoint(TrackedHandJoint.Wrist, out MixedRealityPose wristPose))
            {

                Gizmos.color = g_yrayColor;
                Gizmos.DrawRay(wristPose.Position, wristPose.Up * _length);
                Gizmos.color = g_zrayColor;
                Gizmos.DrawRay(wristPose.Position, wristPose.Forward * _length);
                Gizmos.color = g_xrayColor;
                Gizmos.DrawRay(wristPose.Position, wristPose.Right * _length);
            }
            if (jointedHand.TryGetJoint(TrackedHandJoint.IndexTip, out MixedRealityPose IndexTipPose))
            {

                Gizmos.color = g_yrayColor;
                Gizmos.DrawRay(IndexTipPose.Position, IndexTipPose.Up * _length);
                Gizmos.color = g_zrayColor;
                Gizmos.DrawRay(IndexTipPose.Position, IndexTipPose.Forward * _length);
                Gizmos.color = g_xrayColor;
                Gizmos.DrawRay(IndexTipPose.Position, IndexTipPose.Right * _length);
            }
            if (jointedHand.TryGetJoint(TrackedHandJoint.MiddleTip, out MixedRealityPose MiddleTipPose))
            {

                Gizmos.color = g_yrayColor;
                Gizmos.DrawRay(MiddleTipPose.Position, MiddleTipPose.Up * _length);
                Gizmos.color = g_zrayColor;
                Gizmos.DrawRay(MiddleTipPose.Position, MiddleTipPose.Forward * _length);
                Gizmos.color = g_xrayColor;
                Gizmos.DrawRay(MiddleTipPose.Position, MiddleTipPose.Right * _length);
            }
            if (jointedHand.TryGetJoint(TrackedHandJoint.PinkyTip, out MixedRealityPose PinkyTipPose))
            {

                Gizmos.color = g_yrayColor;
                Gizmos.DrawRay(PinkyTipPose.Position, PinkyTipPose.Up * _length);
                Gizmos.color = g_zrayColor;
                Gizmos.DrawRay(PinkyTipPose.Position, PinkyTipPose.Forward * _length);
                Gizmos.color = g_xrayColor;
                Gizmos.DrawRay(PinkyTipPose.Position, PinkyTipPose.Right * _length);
            }
            if (jointedHand.TryGetJoint(TrackedHandJoint.RingTip, out MixedRealityPose RingTipPose))
            {

                Gizmos.color = g_yrayColor;
                Gizmos.DrawRay(RingTipPose.Position, RingTipPose.Up * _length);
                Gizmos.color = g_zrayColor;
                Gizmos.DrawRay(RingTipPose.Position, RingTipPose.Forward * _length);
                Gizmos.color = g_xrayColor;
                Gizmos.DrawRay(RingTipPose.Position, RingTipPose.Right * _length);
            }
            if (jointedHand.TryGetJoint(TrackedHandJoint.ThumbTip, out MixedRealityPose ThumbTipPose))
            {

                Gizmos.color = g_yrayColor;
                Gizmos.DrawRay(ThumbTipPose.Position, ThumbTipPose.Up * _length);
                Gizmos.color = g_zrayColor;
                Gizmos.DrawRay(ThumbTipPose.Position, ThumbTipPose.Forward * _length);
                Gizmos.color = g_xrayColor;
                Gizmos.DrawRay(ThumbTipPose.Position, ThumbTipPose.Right * _length);
            }
        }
    }
}
