using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnEnemyObject : MonoBehaviour
{

    private AttackManager _attackManager = default;

    private GameObject _targetEnemyObject = default;

    private const string LIST_OBJECT_TAGNAME = "List"; 

    void Start()
    {
        _attackManager = GameObject.FindGameObjectWithTag(LIST_OBJECT_TAGNAME).GetComponent<AttackManager>();
    }

    
    public void clickAttackObject()
    {
        _targetEnemyObject = this.gameObject;
        _attackManager.AttackEvent(_targetEnemyObject);
    }
}
