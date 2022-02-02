using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private int _nowMoney = 0;

    [SerializeField]
    private TextMeshPro _moneyTextUI;


    private void Start()
    {
        MoneyTextUpdate();
    }

    /// <summary>
    /// 所持金のUIを更新
    /// </summary>
    private void MoneyTextUpdate()
    {
        _moneyTextUI.text = "$"+_nowMoney.ToString("D5");
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
            return false;
        }

        _nowMoney -= _value;
        MoneyTextUpdate();
        return true;
    }

    /// <summary>
    /// 現在の所持金を返す
    /// </summary>
    /// <returns></returns>
    public int ReturnNowMoney()
    {
        return _nowMoney;
    }

}
