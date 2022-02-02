using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUISet : MonoBehaviour
{
    [SerializeField,Header("�q�I�u�W�F�N�g��UI������")]
    private GameObject _targetUiObject = default;

    private TargetUIAnimation _targetUIAnimation = default;

    [SerializeField, Header("�G�̃��b�V��")]
    private Renderer _targetRenderer;

    private bool _uiSetFlag = false;

    private Vector3 _UiSetVector3 = new Vector3(1.0f, 1.0f, 1.0f);

    private GameObject _cameraObject = default;

    private const string TAG_NAME_CAMERA = "MainCamera";

    void Start()
    {
        _cameraObject = GameObject.FindGameObjectWithTag(TAG_NAME_CAMERA);
        //�^�[�Q�b�gUI�I�u�W�F�N�g����A�j���[�V�����𓮂���
        _targetUIAnimation = _targetUiObject.GetComponent<TargetUIAnimation>();
    }

    
    void Update()
    {
        //�J�����ɉf���Ă��邩���肵��UI�̃I���I�t��؂�ւ���
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
    /// UI�\�����I���̎��̏���
    /// </summary>
    private void UiOn()
    {
        

        if (!_uiSetFlag)
        {
            _targetUiObject.transform.position = _cameraObject.transform.position;
            _targetUiObject.SetActive(true);
            print("�Ă΂ꂽ");
            //_targetUiObject.transform.localScale = _UiSetVector3;
            _targetUIAnimation.SetFlag(true);
            _uiSetFlag = true;
        }
    }

    /// <summary>
    /// UI�\�����I�t�̂Ƃ��̏���
    /// </summary>
    private void UiOff()
    {
        _targetUIAnimation.SetFlag(false);
        _targetUiObject.SetActive(false);
        _uiSetFlag = false;
    }

}
