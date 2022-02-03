using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLookat : MonoBehaviour
{

    private GameObject _cameraObject = default;

    private const string TAG_NAME_CAMERA = "MainCamera";

    void Start()
    {
        _cameraObject = GameObject.FindGameObjectWithTag(TAG_NAME_CAMERA);
    }

    
    void Update()
    {
        gameObject.transform.LookAt(_cameraObject.transform);
    }
}
