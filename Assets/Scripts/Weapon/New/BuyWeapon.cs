using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _moneyManager;
    private MoneyManager _moneyScript;

    [SerializeField]
    private GameObject _playerObj = null;

    [SerializeField]
    private List<GameObject> _weaponPrefabList = new List<GameObject>();

    private void Start()
    {
        _moneyScript = _moneyManager.GetComponent<MoneyManager>();
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// お金を消費して、武器のプレハブを生成する
    /// </summary>
    /// <param name="_prefab">生成するプレハブ</param>
    /// <param name="_price">値段</param>
    /// <param name="_offset">生成位置</param>
    public void CreateWeaponPrefab(GameObject _prefab,int _price,Vector3 _offset)
    {
        //お金を消費
        //足りない場合は処理終了
        if (!_moneyScript.MoneyMinus(_price)) { return; }

        //生成位置を計算（ Offset+プレイヤー ）
        Vector3 _createPos = _playerObj.transform.position + _offset;
        //プレハブ生成
        Instantiate(_prefab, _createPos, Quaternion.identity);
    }

}
