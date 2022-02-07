using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUiOfset : MonoBehaviour
{

    [SerializeField,Range(1.5f,10.0f)]
    private float _uiOfset = default;

    private Vector3 _uiPosition = new Vector3(0.0f, 0.0f, 1.5f);
    
    void Update()
    {

        _uiPosition.x = transform.localPosition.x;
        _uiPosition.y = transform.localPosition.y;
        _uiPosition.z = _uiOfset;
        transform.localPosition = _uiPosition;
    }
}
