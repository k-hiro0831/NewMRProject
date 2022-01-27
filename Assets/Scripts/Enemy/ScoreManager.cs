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
    /// �X�R�A���Z
    /// </summary>
    /// <param name="_value">���j���̒l</param>
    public void ScoreAdd(int _value)
    {
        _scoreTotal += _value;
    }

    /// <summary>
    /// �X�R�A���v
    /// </summary>
    /// <param name="_value">�X�R�A���v�l</param>
    public int ScoreTotal(int _value)
    {
        _value += _scoreTotal;
        return _value;
    }

    #region"�X�R�A�ݒ�"
    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Fly(int _value)
    {
        _value = _fly;
        return _value;
    }

    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Air(int _value)
    {
        _value = _air;
        return _value;
    }

    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Earth(int _value)
    {
        _value = _earth;
        return _value;
    }

    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Lava(int _value)
    {
        _value = _lava;
        return _value;
    }

    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Water(int _value)
    {
        _value = _water;
        return _value;
    }
    #endregion

}
