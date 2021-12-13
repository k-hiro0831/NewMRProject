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

    void Start()
    {
        
    }

    
    void Update()
    {
        HandFind();
    }

    //private HandStatus CheckCurrentHandStatus(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    //{
    //    IDictionary<TrackedHandJoint, MixedRealityPose> joint = eventData.InputData;
    //    Vector3 none_0 = joint[TrackedHandJoint.None].Position;                  // ‘Î‰ž‚È‚µ
    //    Vector3 wrist_0 = joint[TrackedHandJoint.Wrist].Position;                // ŽèŽñ
    //    Vector3 palm_0 = joint[TrackedHandJoint.Palm].Position;
    //}

    private void HandFind()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose pose))
        {
            IMixedRealityHandJointService handtrackingservice = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
            if (handtrackingservice != null)
            {
                TestObj.transform.position = handtrackingservice.RequestJointTransform(TrackedHandJoint.MiddleKnuckle, Handedness.Right).position;
                TestObj.transform.rotation= handtrackingservice.RequestJointTransform(TrackedHandJoint.MiddleKnuckle, Handedness.Right).rotation;
            }
        }
        else
        {
            Debug.Log("deleta");
        }
    }

    private void HandTrackTest()
    {
        var ko = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
        if (ko != null)
        {
            Debug.Log(ko.RequestJointTransform(TrackedHandJoint.IndexTip, Handedness.Right)); 
        }

    }
}

