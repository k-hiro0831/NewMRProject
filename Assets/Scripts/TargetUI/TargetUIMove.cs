using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUIMove : MonoBehaviour
{
    //----------------------------------------------------------------------------------------------------------------
    //GameObject
    [SerializeField]
    private GameObject _enemyObject = default;
    [SerializeField]
    private GameObject _cameraObject = default;

    private Vector3 _pos = default;

    private RectTransform _rectTransforms = default;

    private Animator _animator = default; 

    //----------------------------------------------------------------------------------------------------------------
    //�O���[�o���ϐ�
    [SerializeField]
    private float _posOfset = 5.0f;

    [SerializeField]
    private float _playerPosAccesMax = 7.0f;

    [SerializeField]
    private float _posDiff = default;

    private bool _setFlag = false;

    private const string ANIME_PARAMETER_STRING_START = "Start";

    void Start()
    {
        _rectTransforms = gameObject.GetComponent<RectTransform>();
        _animator = gameObject.GetComponent<Animator>();
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
    /// TargetUI�𓮂����֐�
    /// </summary>
    public void UiPosUpdate()
    {
        //�G�ƃJ�����̋��������߂�
        //_posDiff = Vector3.Distance(_enemyObject.transform.position, _cameraObject.transform.position);

        ////�J�����Ƃ̋������߂����̏���
        //if (_posDiff <= _playerPosAccesMax)
        //{
        //    _pos = Vector3.Lerp(_enemyObject.transform.position, _cameraObject.transform.position, 0.2f);

        //}
        //�J�����Ƃ̋������������̏���
        //else
        //{
        //    //�P�ʃx�N�g���ɃJ��������b���������|���ăJ���������苗���b���x�N�g���Ƌ��߂�
        //    _pos = (_enemyObject.transform.position - _cameraObject.transform.position).normalized * _posOfset;
        //    //���߂��x�N�g���Ɍ��݂̃J�����|�W�V��������ɂ���
        //    _pos = _pos + _cameraObject.transform.position;


        //}
        ////�ʒu�X�V�A���b�N�A�b�g
        //_pos = (_enemyObject.transform.position - _cameraObject.transform.position).normalized * _posOfset;
        //_pos = _pos + _cameraObject.transform.position;
        _pos = Vector3.Lerp(_enemyObject.transform.position, _cameraObject.transform.position, 0.2f);
        _rectTransforms.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _pos);
        //gameObject.transform.position = _pos;
        //transform.LookAt(_enemyObject.transform.position);


    }

    /// <summary>
    /// targetUi�̓������~�߂鏈��
    /// �A�C�g���b�L���O�̃X�g�b�v�̏����ŌĂ΂��
    /// </summary>
    public void UiMoveStop()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// targetUi�̓������n�߂鏈��
    /// �A�C�g���b�L���O�̃X�^�[�g�̏����ŌĂ΂��
    /// </summary>
    public void UiMoveStart()
    {
        
        this.gameObject.SetActive(true);
        _animator.SetTrigger(ANIME_PARAMETER_STRING_START);
    }
}
