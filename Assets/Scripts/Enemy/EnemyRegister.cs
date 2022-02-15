using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegister : MonoBehaviour
{
    [SerializeField, Header("各種類の敵をここに追加（全21種類）")]
    private GameObject[] _enemys = default;

    private Vector3 _originPosition;

    void Start()
    {
        //各オブジェクトに格納
        //for (int i = 0; i < 21; i++)
        //{
        //    //各登録数は10
        //    for (int j = 0; j < 10; j++)
        //    {
        //        GameObject obj = GameObject.Instantiate(_enemys[i], _originPosition, Quaternion.identity);
        //        obj.transform.parent = this.transform.GetChild(i).gameObject.transform;
        //        obj.SetActive(false);
                
        //    }
            
        //}
    }
}
