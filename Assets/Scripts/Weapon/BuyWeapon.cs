using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _moneyManager;
    private MoneyManager _moneyScript;

    [SerializeField]
    private GameObject _nowWeapon = null;

    [SerializeField]
    private List<GameObject> _weaponButtonList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _weaponPrefabList = new List<GameObject>();

    [SerializeField]
    private GameObject _playerObj = null;

    [SerializeField]
    private int _selectNum = 0;
    [SerializeField]
    private string _selectName = null;
    [SerializeField]
    private int _selectPrice = 0;
    [SerializeField]
    private int _selectPower = 0;
    [SerializeField]
    private int _selectRate = 0;
    [SerializeField]
    private GameObject _selectPrefab = null;
    [SerializeField]
    private bool _selectIsLock = false;

    [SerializeField]
    private GameObject _weaponDetailUI = null;
    private WeaponDetailUI _uiScript;

    [SerializeField]
    private GameObject _listObj = null;
    private CllickObjectList _clickObjScript;

    [SerializeField]
    private GameObject _createPosObj = null;

    private void Start()
    {
        _moneyScript = _moneyManager.GetComponent<MoneyManager>();
        _uiScript = _weaponDetailUI.GetComponent<WeaponDetailUI>();
        _clickObjScript = _listObj.GetComponent<CllickObjectList>();

        SelectedWeapon(0, 0, false);
        BuySelectedWeapon();
        _nowWeapon = _weaponPrefabList[_selectNum];

        _nowWeapon.SetActive(true);
    }

    /// <summary>
    /// 武器のUIを選択する処理
    /// </summary>
    /// <param name="_num">指標</param>
    /// <param name="_price">値段</param>
    /// <param name="_lock">ロック状態かどうか</param>
    public void SelectedWeapon(int _num, int _price, bool _lock)
    {
        //選択中の情報を更新
        _selectNum = _num;
        _selectPrice = _price;
        _selectIsLock = _lock;
        _selectPrefab = _weaponPrefabList[_selectNum];

        //詳細UIの情報を変更する
        (_selectPower, _selectRate) = _selectPrefab.GetComponent<GunShot>().ReturnWeaponStatus();
        _selectName = _selectPrefab.GetComponent<GunShot>().ReturnWeaponName();

        //詳細UIを更新
        _uiScript.UpdateWeaponUI(_selectName, _selectPrice, _selectPower, _selectRate, _selectIsLock);
    }

    /// <summary>
    /// 選択中の武器を購入する
    /// </summary>
    public void BuySelectedWeapon()
    {
        //ロックされている場合に処理
        if (_selectIsLock)
        {
            //お金を消費
            //足りない場合は処理終了
            if (!_moneyScript.MoneyMinus(_selectPrice)) { return; }

            //アンロックする
            Debug.Log("アンロック完了");
            _weaponButtonList[_selectNum].GetComponent<WeaponSelected>().UnLock();
            _selectIsLock = false;
            //詳細UIを更新
            _uiScript.UpdateWeaponUI(_selectName, _selectPrice, _selectPower, _selectRate, _selectIsLock);
        }

        //武器入れ替え
        //選択中の武器を非表示
        _nowWeapon.SetActive(false);
        //次の武器を表示し、位置を変更
        _weaponPrefabList[_selectNum].SetActive(true);
        //位置と角度を変更
        _weaponPrefabList[_selectNum].transform.position = _createPosObj.transform.position;
        _weaponPrefabList[_selectNum].transform.rotation =
            Quaternion.Euler(new Vector3(0, _playerObj.transform.localEulerAngles.y, _playerObj.transform.localEulerAngles.z));

        _nowWeapon = _weaponPrefabList[_selectNum];

        //所持武器の入れ替え
        _clickObjScript.WeaponListClear();
        _clickObjScript.WeaponObjectSet(_nowWeapon);

    }
}