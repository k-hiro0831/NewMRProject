using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;


public class EnemyObjectCllick : MonoBehaviour
{

    private CllickObjectList cllickObjectList = default;

    private const string LIST_OBJECT = "List";

    private void Awake()
    {
        //リストを管理しているオブジェクトを取得
        cllickObjectList = GameObject.FindGameObjectWithTag(LIST_OBJECT).GetComponent<CllickObjectList>();
    }

    /// <summary>
    /// オブジェクトのpointerHandlerのOnPointerDownから呼び出す
    /// リストにオブジェクトを追加
    /// </summary>
    /// <param name="eventData"></param>
    public void EnemyCllickEvent(MixedRealityPointerEventData eventData)
    {
        cllickObjectList.EnemyObjSet(this.gameObject);
    }

}
