using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSelected : MonoBehaviour
{
    [SerializeField]
    private int _number;
    [SerializeField]
    private int _price;
    [SerializeField]
    private bool _isLock = true;

    private BuyWeapon _buyScript;

    [SerializeField]
    private GameObject _priceObj;
    [SerializeField]
    private GameObject _filterObj;

    private void Start()
    {
        _buyScript = GameObject.Find("WeaponManager").GetComponent<BuyWeapon>();
        if (_isLock == false)
        {
            _filterObj.SetActive(false);
            _priceObj.SetActive(false);
        }
        _priceObj.GetComponent<TextMeshPro>().text = "$"+_price.ToString("D4");
    }

    private void Update()
    {

    }

    public void SelectWeapon()
    {
        _buyScript.SelectedWeapon(_number, _price, _isLock);
    }

    public void UnLock()
    {
        _isLock = false;
        _filterObj.SetActive(false);
        _priceObj.SetActive(false);
    }

}
