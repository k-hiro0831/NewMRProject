using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFloat : MonoBehaviour
{

    [SerializeField]
    private bool _floatSet = false;

    private Vector3 _posVector = default;

    [SerializeField]
    private float _hoverSpeed = default;

    [SerializeField]
    private float _hoverWidth = default;

    void Start()
    {
        
    }

   
    private void Update()
    {
        
        if (_floatSet)
        {
            FloatWeaponUpdate();
        }
        
    }

    public void HoverSet(bool set)
    {
        _floatSet = set;
    }

    private void FloatWeaponUpdate()
    {

        //float sin = Mathf.Sin(Time.time);
        //print(sin);

        //_posVector.y =  sin;
        
        //transform.position = _posVector;

        _posVector = transform.position;
        _posVector.y += Mathf.Sin(Time.time * _hoverSpeed) * _hoverWidth * Time.deltaTime;
        transform.position = _posVector;

    }

}
