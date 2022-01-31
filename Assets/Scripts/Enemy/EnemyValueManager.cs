using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValueManager : MonoBehaviour
{
    [SerializeField]
    private int _scoreTotal;

    [SerializeField]
    private int _flyScore;
    [SerializeField]
    private int _airScore;
    [SerializeField]
    private int _earthScore;
    [SerializeField]
    private int _waterScore;
    [SerializeField]
    private int _lavaScore;

    [SerializeField]
    private int _flyMoney;
    [SerializeField]
    private int _airMoney;
    [SerializeField]
    private int _earthMoney;
    [SerializeField]
    private int _waterMoney;
    [SerializeField]
    private int _lavaMoney;

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="_value">撃破時の値</param>
    public void ScoreAdd(int _value)
    {
        _scoreTotal += _value;
    }

    /// <summary>
    /// スコア合計
    /// </summary>
    /// <param name="_value">スコア合計値</param>
    public int ScoreTotal(int _value)
    {
        _value = _scoreTotal;
        return _value;
    }

    #region"スコア設定"
    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Fly(int _value)
    {
        _value = _flyScore;
        return _value;
    }

    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Air(int _value)
    {
        _value = _airScore;
        return _value;
    }

    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Earth(int _value)
    {
        _value = _earthScore;
        return _value;
    }

    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Lava(int _value)
    {
        _value = _lavaScore;
        return _value;
    }

    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Water(int _value)
    {
        _value = _waterScore;
        return _value;
    }
    #endregion

    #region"お金設定"
    /// <summary>
    /// 敵のお金設定
    /// </summary>
    /// <param name="_value">各敵のお金</param>
    public int FlyMoney(int _value)
    {
        _value = _flyMoney;
        return _value;
    }

    /// <summary>
    /// 敵のお金設定
    /// </summary>
    /// <param name="_value">各敵のお金</param>
    public int AirMoney(int _value)
    {
        _value = _airMoney;
        return _value;
    }

    /// <summary>
    /// 敵のお金設定
    /// </summary>
    /// <param name="_value">各敵のお金</param>
    public int EarthMoney(int _value)
    {
        _value = _earthMoney;
        return _value;
    }

    /// <summary>
    /// 敵のお金設定
    /// </summary>
    /// <param name="_value">各敵のお金</param>
    public int LavaMoney(int _value)
    {
        _value = _lavaMoney;
        return _value;
    }

    /// <summary>
    /// 敵のお金設定
    /// </summary>
    /// <param name="_value">各敵のお金</param>
    public int WaterMoney(int _value)
    {
        _value = _waterMoney;
        return _value;
    }
    #endregion
}
