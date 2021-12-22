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

    //エフェクト生成位置
    private Vector3 _createPos = Vector3.zero;

    private void Start()
    {
        //ゲージを初期化
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
        //ポイントが足りないとき処理終了
        if(_skillPoint < _skillPointMax) 
        {
            Debug.Log("ゲージ不足");
            return; 
        }

        Debug.Log("スキル発動");
        //技エフェクト生成
        Instantiate(_skillList[_selectSkillNumber], _createPos, Quaternion.identity);

        //ゲージを初期化
        _skillPoint = 0;
        _skillImage.fillAmount = 0;
    }

    /// <summary>
    /// 必殺技ゲージを上昇させる処理
    /// </summary>
    /// <param name="_value">上昇量</param>
    public void PlusSkillGauge(float _value)
    {
        _skillPoint += _value;
        //上限を超えたとき
        if (_skillPoint > _skillPointMax)
        {
            _skillPoint = _skillPointMax;
        }
        //ゲージに値を反映
        _skillImage.fillAmount = _skillPoint / 100;
    }
}
