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

    [SerializeField,Header("�Z�b�g����Ƃ��̓����X�s�[�h")]
    private float _setSpeed = default;

    private const string TAG_NAME_CAMERA = "MainCamera";

    void Start()
    {
        _cameraObject = GameObject.FindGameObjectWithTag(TAG_NAME_CAMERA);
        _targetUiAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_setFlag)
        {
            SetUiMove();
        }
    }

    /// <summary>
    /// �J�����ɓ��������Ɉ��̃I�u�W�F�N�g�̃��b�N�A�b�g����Ă΂��
    /// </summary>
    public void SetFlag(bool i)
    {
        if (i)
        {
            _setFlag = true;
        }
        else
        {
            _setFlag = false;
        }
        
    }

    /// <summary>
    /// �Z�b�g����Ƃ��̓���
    /// </summary>
    private void SetUiMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, _uiPosition, Time.deltaTime);
        
    }

    /// <summary>
    /// �^�[�Q�b�gUI�̃Z�b�g�A�j���[�V�������Ăяo��
    /// </summary>
    public void UiSetAnimetionStart()
    {
        //print("�Ă΂ꂽ");
        
        _targetUiAnimator.SetTrigger(UI_SET_ANIME_TRIGER);
    }
}
