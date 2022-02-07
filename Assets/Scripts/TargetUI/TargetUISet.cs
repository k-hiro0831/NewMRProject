using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUISet : MonoBehaviour
{
    [SerializeField,Header("子オブジェクトのUIを入れる")]
    private GameObject _targetUiObject = default;

    private TargetUIAnimation _targetUIAnimation = default;

    [SerializeField, Header("敵のメッシュ")]
    private Renderer _targetRenderer;

    private bool _uiSetFlag = false;

    private Vector3 _UiSetVector3 = new Vector3(1.0f, 1.0f, 1.0f);

    private GameObject _cameraObject = default;

    private const string TAG_NAME_CAMERA = "MainCamera";

    void Start()
    {
        _cameraObject = GameObject.FindGameObjectWithTag(TAG_NAME_CAMERA);
        //ターゲットUIオブジェクトからアニメーションを動かす
        _targetUIAnimation = _targetUiObject.GetComponent<TargetUIAnimation>();
    }

    
    void Update()
    {
        //カメラに映っているか判定してUIのオンオフを切り替える
        if (_targetRenderer.isVisible)
        {
            UiOn();
        }
        else
        {
            UiOff();
        }
    }

    /// <summary>
    /// UI表示がオンの時の処理
    /// </summary>
    private void UiOn()
    {
        

        if (!_uiSetFlag)
        {
            _cameraObject.SetActive(true);
            
            
            _targetUIAnimation.UiSetAnimetionStart();
            _uiSetFlag = true;
        }
    }

    /// <summary>
    /// UI表示がオフのときの処理
    /// </summary>
    private void UiOff()
    {
        _cameraObject.SetActive(false);

        _uiSetFlag = false;
    }

}
