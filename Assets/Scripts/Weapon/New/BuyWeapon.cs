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
    private GameObject _nowWeapon = null;

    [SerializeField]
    private List<GameObject> _weaponButtonList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _weaponPrefabList = new List<GameObject>();

    [SerializeField]
    private int _selectNum = 0;
    [SerializeField]
    private int _selectPrice = 0;
    [SerializeField]
    private GameObject _selectPrefab = null;
    [SerializeField]
    private bool _selectIsLock = false;


    private void Start()
    {
        _moneyScript = _moneyManager.GetComponent<MoneyManager>();

        SelectedWeapon(0, 0, false);
        BuySelectedWeapon();
        _nowWeapon = _weaponPrefabList[_selectNum];

        _nowWeapon.SetActive(true);

    }

    private void Update()
    {
        
    }

    public void SelectedWeapon(int _num,int _price,bool _lock)
    {
        _selectNum = _num;
        _selectPrice = _price;
        _selectIsLock = _lock;
        _selectPrefab = _weaponPrefabList[_selectNum];
    }

    /// <summary>
    /// 選択中の武器を購入する
    /// </summary>
    public void BuySelectedWeapon()
    {
        //ロック中ではないとき
        if (!_selectIsLock)
        {
            //お金消費なし
            //武器入れ替え

            //選択中の武器を非表示
            _nowWeapon.SetActive(false);
            //次の武器を表示し、位置を変更
            _weaponPrefabList[_selectNum].SetActive(true);
            _weaponPrefabList[_selectNum].transform.position = _nowWeapon.transform.position;
            _nowWeapon = _weaponPrefabList[_selectNum];

            Debug.Log("アンロック済み");
        }

        //お金を消費
        //足りない場合は処理終了
        if (!_moneyScript.MoneyMinus(_selectPrice)) { return; }

        //アンロックする
        Debug.Log("アンロック完了");
        _weaponButtonList[_selectNum].GetComponent<WeaponSelected>().UnLock();
        _selectIsLock = false;

        //武器入れ替え
        //選択中の武器を非表示
        _nowWeapon.SetActive(false);
        //次の武器を表示し、位置を変更
        _weaponPrefabList[_selectNum].SetActive(true);
        _weaponPrefabList[_selectNum].transform.position = _nowWeapon.transform.position;
        _nowWeapon = _weaponPrefabList[_selectNum];

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

        //アンロック状態にする
    }

}
