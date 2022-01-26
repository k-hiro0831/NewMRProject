using UnityEngine;

public class AttackManager : MonoBehaviour
{
    
    private CllickObjectList _cllickObjectList = default;

    private CameraRay _cameraRay = default;

    private GameObject _enemyObj = default;

    [SerializeField]
    private TargetUIMove _targetUIMove = default;

    [SerializeField,Header("�^�[�Q�b�gUI�̃A�j���[�^�[")]
    private Animator _targetUiAnimator = default;

    //-------------------------------------------------------------------------------------------------------------
    //�ϐ�
    [SerializeField]
    private float _coolTime = 0.0f;
    [SerializeField]
    private bool _coolTimeFlag = false;

    private bool _uiFlag = false;

    //-------------------------------------------------------------------------------------------------------------
    //const
    private const string CAMERA_TAG = "MainCamera";
    private const string UI_ANIMATOR_TRIGGER_ATTACK = "Attack";
    private const string UI_ANIMATOR_BOOL_ATTCKHOLD = "AttackHold";
    private const float COOL_TIME_MAX = 3.0f;

    private void Start()
    {
        _cameraRay = GameObject.FindGameObjectWithTag(CAMERA_TAG).GetComponent<CameraRay>();
        _cllickObjectList = gameObject.GetComponent<CllickObjectList>();
    }

    
    private void Update()
    {
        AttackEvent();

    }

    /// <summary>
    /// �U���C�x���g���Ăяo���֐�
    /// </summary>
    public void AttackEvent()
    {
        //TODO:�����ɍU���̃C�x���g������
        for (int i = 0; i < _cllickObjectList._weaponObjList.Count; i++)
        {
            //TODO:_enemyObj��this.gameobject�ɕύX
            _cllickObjectList._weaponObjList[i].GetComponent<WeaponAttack>().Attack(_enemyObj);
        }
        //TODO:�^�[�Q�b�g�A�j���[�V�����֌W
        ////�^�[�Q�b�gUI�̃A�j���[�V�������Ăяo��
        //_targetUiAnimator.SetTrigger(UI_ANIMATOR_TRIGGER_ATTACK);
        //_targetUiAnimator.SetBool(UI_ANIMATOR_BOOL_ATTCKHOLD, true);
        ObjListClear();
        

    }

    /// <summary>
    /// ���X�g������������֐�
    /// </summary>
    private void ObjListClear()
    {
        for (int i = 0; i < _cllickObjectList._weaponObjList.Count ; i++)
        {
            _cllickObjectList._weaponObjList[i].GetComponent<WeaponFloat>().HoverSet(false);
        }
        _cllickObjectList.EnemyListClear();
        _cllickObjectList.WeaponListClear();

        _targetUiAnimator.SetBool(UI_ANIMATOR_BOOL_ATTCKHOLD, false);
    }

    
}
