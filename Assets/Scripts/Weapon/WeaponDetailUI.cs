using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponDetailUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _nameText;
    [SerializeField]
    private TextMeshPro _priceText;

    [SerializeField]
    private List<GameObject> _powerList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _rateList = new List<GameObject>();

    private void Start()
    {
        //StatusClear();
    }

    private void Update()
    {
        
    }

    public void UpdateWeaponUI(string _name,int _price,int _power,int _rate,bool _isLock)
    {
        _nameText.text = _name;

        if (_isLock)
        {
            _priceText.text = "$"+_price.ToString("D4");
        }
        else
        {
            _priceText.text = "";
        }
        

        StatusClear();

        for (int i = 0; i < _power; i++)
        {
            _powerList[i].SetActive(true);
        }

        for (int i = 0; i < _rate; i++)
        {
            _rateList[i].SetActive(true);
        }
    }

    /// <summary>
    /// パワーとレートのUIをすべて非表示にする
    /// </summary>
    private void StatusClear()
    {
        for (int i = 0; i < _powerList.Count; i++)
        {
            _powerList[i].SetActive(false);
        }
        for (int i = 0; i < _rateList.Count; i++)
        {
            _rateList[i].SetActive(false);
        }
    }
}
