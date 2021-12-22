using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CllickObjectList : MonoBehaviour
{
    //-------------------------------------------------------------------------------------------------------------
    //選択している敵と武器のオブジェクトのリスト
    public List<GameObject> _weaponObjList = new List<GameObject>();

    public List<GameObject> _enemyObjList = new List<GameObject>();


    /// <summary>
    /// 武器のオブジェクトを選択したときに選択武器のリストにオブジェクトを追加
    /// 武器オブジェクトから呼び出す
    /// </summary>
    /// <param name="obj">追加するオブジェクト</param>
    public void WeaponObjectSet(GameObject obj)
    {
        if (_weaponObjList.Contains(obj))
        {
            
        }
        else
        {
            //リストにオブジェクト追加
            _weaponObjList.Add(obj);
        }

    }

    /// <summary>
    /// 敵オブジェクトを選択したときにオブジェクトをリストに格納
    /// 敵オブジェクトから呼び出す
    /// </summary>
    /// <param name="obj"></param>
    public void EnemyObjSet(GameObject obj)
    {
        if (_enemyObjList.Contains(obj))
        {

        }
        else
        {
            //リストにオブジェクト追加
            _enemyObjList.Add(obj);
        }
    }

    /// <summary>
    /// 攻撃武器リストをクリアする
    /// </summary>
    public void WeaponListClear()
    {
        
        _weaponObjList.Clear();
    }

    /// <summary>
    /// 敵リストをクリアする
    /// </summary>
    public void EnemyListClear()
    {
        
        _enemyObjList.Clear();
    }
}
