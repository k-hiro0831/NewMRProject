using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttract : MonoBehaviour
{
    [SerializeField]
    private GameObject _rayObj;
    private AttractRay _rayScript;

    [SerializeField]
    private GameObject _handObj;
    [SerializeField]
    private Vector3 _handObjPos;
    [SerializeField]
    private GameObject _targetObj_Test;
    [SerializeField]
    private float _attractSpeed = 10f;

    private void Start()
    {
        _rayScript = _rayObj.GetComponent<AttractRay>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attract();
        }
    }

    //指定したアイテムを指定した位置にひきよせる
    private void Attract()
    {
        if (_rayScript.ReturnIsHit() == false) { return; }
        _targetObj_Test = _rayScript.ReturnHitObj();
        if (_targetObj_Test == null) { return; }

        //仮置きで例の開始位置にしている
        _handObjPos = this.transform.position;
        StartCoroutine(ItemMove());
    }

    private IEnumerator ItemMove()
    {
        while(_targetObj_Test.transform.position != _handObjPos)
        {
            float _step = _attractSpeed * Time.deltaTime;
            _targetObj_Test.transform.position = 
                Vector3.MoveTowards(_targetObj_Test.transform.position, _handObjPos, _step);
            yield return null;
        }
        yield break;
    }
}
