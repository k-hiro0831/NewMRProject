using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    //TODO:テスト終わったら消します

    [SerializeField,Range(-5.0f,5.0f)]
    private float _positionX = default;

    private Vector3 _vector3 = default;

    
    private void Update()
    {
        _vector3.x = _positionX;
        _vector3.y = transform.position.y;
        _vector3.z = transform.position.z;

        transform.position = _vector3;
    }
}
