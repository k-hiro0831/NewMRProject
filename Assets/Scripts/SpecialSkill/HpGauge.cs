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
    [SerializeField]
    private float _hpMax = 100f;


    private void Start()
    {
        _hp = _hpMax;
        _hpImage = _hpImageObj.GetComponent<Image>();
        _hpImage.fillAmount = _hp / 100;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlusHp(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            MinusHp(10);
        }
    }

    /// <summary>
    /// HP�𑝂₷����
    /// </summary>
    /// <param name="_value">�㏸��</param>
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
    /// HP�����炷����
    /// </summary>
    /// <param name="_value">������</param>
    public void MinusHp(float _value)
    {
        _hp -= _value;
        if (_hp <= 0)
        {
            _hp = 0;
            Debug.Log("�Q�[���I�[�o�[�ł�");
        }
        _hpImage.fillAmount = _hp / 100;
    }
}
