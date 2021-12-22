using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialSkill : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _skillList = new List<GameObject>();
    [SerializeField]
    private int _selectSkillNumber = 0;

    [SerializeField]
    private GameObject _skillImageObj;
    private Image _skillImage;

    [SerializeField]
    private float _skillPoint = 0f;
    [SerializeField]
    private float _skillPointMax = 100f;

    //�G�t�F�N�g�����ʒu
    private Vector3 _createPos = Vector3.zero;

    private void Start()
    {
        //�Q�[�W��������
        _skillPoint = 0;
        _skillImage = _skillImageObj.GetComponent<Image>();
        _skillImage.fillAmount = 0;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SkillActivate();
        //}

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    PlusSkillGauge(10);
        //}
    }

    public void SkillActivate()
    {
        //�|�C���g������Ȃ��Ƃ������I��
        if(_skillPoint < _skillPointMax) 
        {
            Debug.Log("�Q�[�W�s��");
            return; 
        }

        Debug.Log("�X�L������");
        //�Z�G�t�F�N�g����
        Instantiate(_skillList[_selectSkillNumber], _createPos, Quaternion.identity);

        //�Q�[�W��������
        _skillPoint = 0;
        _skillImage.fillAmount = 0;
    }

    /// <summary>
    /// �K�E�Z�Q�[�W���㏸�����鏈��
    /// </summary>
    /// <param name="_value">�㏸��</param>
    public void PlusSkillGauge(float _value)
    {
        _skillPoint += _value;
        //����𒴂����Ƃ�
        if (_skillPoint > _skillPointMax)
        {
            _skillPoint = _skillPointMax;
        }
        //�Q�[�W�ɒl�𔽉f
        _skillImage.fillAmount = _skillPoint / 100;
    }
}
