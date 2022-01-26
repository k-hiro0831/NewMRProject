using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField]
    private Text _priceText;
    [SerializeField]
    private GameObject _weaponPrefab = null;
    [SerializeField]
    private int _weaponPrice = 0;
    [SerializeField]
    private Vector3 _createOffset = Vector3.zero;

    private BuyWeapon _buyScript;

    private void Start()
    {
        _buyScript = GameObject.Find("WeaponManager").GetComponent<BuyWeapon>();
        _priceText.text = _weaponPrice.ToString("D4");
    }

    public  GameObject ReturnPrefab()
    {
        return _weaponPrefab;
    }

    public int ReturnPrice()
    {
        return _weaponPrice;
    }
}
