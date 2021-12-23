using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUIMove : MonoBehaviour
{
    //----------------------------------------------------------------------------------------------------------------
    //GameObject
    
    private GameObject _enemyObject = default;
    [SerializeField]
    private GameObject _cameraObject = default;

    private CameraRay _cameraRay = default;

    private Vector3 _pos = default;

    private RectTransform _rectTransforms = default;

    private Animator _animator = default; 

    //----------------------------------------------------------------------------------------------------------------
    //グローバル変数
    [SerializeField]
    private float _posOfset = 5.0f;

    [SerializeField]
    private float _playerPosAccesMax = 7.0f;

    [SerializeField]
    private float _posDiff = default;

    private bool _setFlag = false;

    private const string ANIME_PARAMETER_STRING_START = "Start";

    private const string TAG_NAME_CAMERA = "MainCamera";

    void Start()
    {
        _rectTransforms = gameObject.GetComponent<RectTransform>();
        _animator = gameObject.GetComponent<Animator>();
        _cameraRay = GameObject.FindWithTag(TAG_NAME_CAMERA).GetComponent<CameraRay>();
    }

    
    void Update()
    {
        if (_setFlag)
        {
            UiPosUpdate();

            //lookat

        }
        else
        {
            //gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// TargetUIを動かす関数
    /// </summary>
    public void UiPosUpdate()
    {

        _enemyObject = _cameraRay.GetHitEnemyObj();

        _pos = Vector3.Lerp(_enemyObject.transform.position, _cameraObject.transform.position, 0.2f);
        _rectTransforms.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _pos);
        //gameObject.transform.position = _pos;
        //transform.LookAt(_enemyObject.transform.position);


    }

    /// <summary>
    /// targetUiの動きを止める処理
    /// アイトラッキングのストップの処理で呼ばれる
    /// </summary>
    public void UiMoveStop()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// targetUiの動きを始める処理
    /// アイトラッキングのスタートの処理で呼ばれる
    /// </summary>
    public void UiMoveStart()
    {
        
        this.gameObject.SetActive(true);
        _animator.SetTrigger(ANIME_PARAMETER_STRING_START);
    }
}
