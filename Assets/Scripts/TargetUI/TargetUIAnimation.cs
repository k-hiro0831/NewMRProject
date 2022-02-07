using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUIAnimation : MonoBehaviour
{
    private Animator _targetUiAnimator = default;

    private const string UI_SET_ANIME_TRIGER = "SetUi";

    private Vector3 _uiPosition = new Vector3(0.0f, 0.0f, 10.0f);

    private GameObject _cameraObject = default;

    private bool _setFlag = false;

    private bool _setStartPositionSet = false;

    private float _distance_two = default;

    [SerializeField,Header("セットするときの動くスピード")]
    private float _setSpeed = default;

    private const string TAG_NAME_CAMERA = "MainCamera";

    void Start()
    {
        
        _targetUiAnimator = gameObject.GetComponent<Animator>();
    }
    

    /// <summary>
    /// ターゲットUIのセットアニメーションを呼び出す
    /// </summary>
    public void UiSetAnimetionStart()
    {
        //print("呼ばれた");
        
        _targetUiAnimator.SetTrigger(UI_SET_ANIME_TRIGER);
    }
}
