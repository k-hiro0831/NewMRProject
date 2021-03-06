using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    [SerializeField]
    private GameObject _hpImageObj;
    private Image _hpImage;

    [SerializeField]
    private float _hp = 0f;
    private float _hpMax;

    [SerializeField]
    private Player _player;
    [SerializeField]
    private NewEnemyControl _enem;

    private const int _hpMinus = 1;

    private void Start()
    {
        _hpMax = _player._playerHp;
        _hp = _hpMax;
        _hpImage = _hpImageObj.GetComponent<Image>();
        _hpImage.fillAmount = _hp / 100;
    }

    /// <summary>
    /// HPðâ·
    /// </summary>
    /// <param name="_value">ã¸Ê</param>
    public void PlusHp(float _value)
    {
        _hp += _value;
        if (_hp > _hpMax)
        {
            _hp = _hpMax;
        }
        _hpImage.fillAmount = _hp / 100;
    }

    /// <summary>
    /// HPð¸ç·
    /// </summary>
    /// <param name="_value">¸­Ê</param>
    public void MinusHp(float _value)
    {
        _hp -= _value;
        if (_hp <= 0)
        {
            _hp = 0;
            _enem.GameState(2);
        }
        _hpImage.fillAmount = _hp / 100;
    }
}
