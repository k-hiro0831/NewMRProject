using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArrowMove : MonoBehaviour
{
    [SerializeField]
    private GameObject _cameraObj = default;

    private Vector3 _UIRotetaVector = default;

    void Start()
    {
        
    }


    void Update()
    {

        _UIRotetaVector.x = 0f;
        _UIRotetaVector.y = 0f;
        _UIRotetaVector.z = _cameraObj.transform.localEulerAngles.y;
        
        gameObject.transform.localEulerAngles = _UIRotetaVector;

    }
}
