using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private int _nowMoney = 0;

    [SerializeField]
    private Text _moneyTextUI;


    private void Start()
    {
        MoneyTextUpdate();
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// 所持金のUIを更新
    /// </summary>
    private void MoneyTextUpdate()
    {
        _moneyTextUI.text = _nowMoney.ToString("D5");
    }

    public void MoneyPlus(int _value)
    {
        _nowMoney += _value;
        MoneyTextUpdate();
    }

    /// <summary>
    /// お金を減らす処理、お金が足りないときはFalseを返す
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public bool MoneyMinus(int _value)
    {
        if (_nowMoney < _value)
        {
            Debug.Log("金が足りない");
            return false;
        }

        Debug.Log("金は問題なし");
        _nowMoney -= _value;
        MoneyTextUpdate();
        return true;
    }

    public int ReturnNowMoney()
    {
        return _nowMoney;
    }

}
