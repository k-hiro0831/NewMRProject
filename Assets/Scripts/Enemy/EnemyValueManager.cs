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

    [SerializeField]
    private int _flyAtk;
    [SerializeField]
    private int _airAtk;
    [SerializeField]
    private int _earthAtk;
    [SerializeField]
    private int _waterAtk;
    [SerializeField]
    private int _lavaAtk;

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
        _value = _scoreTotal;
        return _value;
    }

    #region"�X�R�A�ݒ�"
    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Fly(int _value)
    {
        _value = _flyScore;
        return _value;
    }

    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Air(int _value)
    {
        _value = _airScore;
        return _value;
    }

    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Earth(int _value)
    {
        _value = _earthScore;
        return _value;
    }

    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Lava(int _value)
    {
        _value = _lavaScore;
        return _value;
    }

    /// <summary>
    /// �G�̃X�R�A�ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̃X�R�A�l</param>
    public int Water(int _value)
    {
        _value = _waterScore;
        return _value;
    }
    #endregion

    #region"�����ݒ�"
    /// <summary>
    /// �G�̂����ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̂���</param>
    public int FlyMoney(int _value)
    {
        _value = _flyMoney;
        return _value;
    }

    /// <summary>
    /// �G�̂����ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̂���</param>
    public int AirMoney(int _value)
    {
        _value = _airMoney;
        return _value;
    }

    /// <summary>
    /// �G�̂����ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̂���</param>
    public int EarthMoney(int _value)
    {
        _value = _earthMoney;
        return _value;
    }

    /// <summary>
    /// �G�̂����ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̂���</param>
    public int LavaMoney(int _value)
    {
        _value = _lavaMoney;
        return _value;
    }

    /// <summary>
    /// �G�̂����ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̂���</param>
    public int WaterMoney(int _value)
    {
        _value = _waterMoney;
        return _value;
    }
    #endregion

    #region"�G�̍U���͐ݒ�"
    /// <summary>
    /// �G�̍U���͐ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̍U����</param>
    public int FlyAtk(int _value)
    {
        _value = _flyAtk;
        return _value;
    }

    /// <summary>
    /// �G�̍U���͐ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̍U����</param>
    public int AirAtk(int _value)
    {
        _value = _airAtk;
        return _value;
    }

    /// <summary>
    /// �G�̍U���͐ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̍U����</param>
    public int EarthAtk(int _value)
    {
        _value = _earthAtk;
        return _value;
    }

    /// <summary>
    /// �G�̍U���͐ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̍U����</param>
    public int LavaAtk(int _value)
    {
        _value = _lavaAtk;
        return _value;
    }

    /// <summary>
    /// �G�̍U���͐ݒ�
    /// </summary>
    /// <param name="_value">�e�G�̍U����</param>
    public int WaterAtk(int _value)
    {
        _value = _waterAtk;
        return _value;
    }
    #endregion
}
