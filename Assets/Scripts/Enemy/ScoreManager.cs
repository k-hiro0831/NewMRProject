using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int _scoreTotal;
    [SerializeField]
    private int _fly;
    [SerializeField]
    private int _air;
    [SerializeField]
    private int _earth;
    [SerializeField]
    private int _water;
    [SerializeField]
    private int _lava;

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
        _value += _scoreTotal;
        return _value;
    }

    #region"スコア設定"
    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Fly(int _value)
    {
        _value = _fly;
        return _value;
    }

    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Air(int _value)
    {
        _value = _air;
        return _value;
    }

    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Earth(int _value)
    {
        _value = _earth;
        return _value;
    }

    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Lava(int _value)
    {
        _value = _lava;
        return _value;
    }

    /// <summary>
    /// 敵のスコア設定
    /// </summary>
    /// <param name="_value">各敵のスコア値</param>
    public int Water(int _value)
    {
        _value = _water;
        return _value;
    }
    #endregion

}
