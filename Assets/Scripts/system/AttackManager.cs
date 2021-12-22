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
    private void AttackEvent()
    {
        //ray���������Ă��邩����
        if (_cameraRay.RayHit())
        {
            //�G�I�u�W�F�N�g�擾
            _enemyObj = _cameraRay.GetHitEnemyObj();
            //�G�I�u�W�F�N�g�����X�g�ɒǉ�
            _cllickObjectList.EnemyObjSet(_enemyObj);
            if (!_uiFlag)
            {
                _targetUIMove.UiMoveStart();
                _uiFlag = true;
            }
            _targetUIMove.UiPosUpdate();

            _coolTime += Time.deltaTime;
            
            //�N�[���^�C�����}�b�N�X���傫�����flag��true�ɂ���
            if (_coolTime > COOL_TIME_MAX)
            {
                _coolTimeFlag = true;
            }

        }
        else
        {
            //���b�N���O�ꂽ���̏�����
            _uiFlag = false;
            _targetUIMove.UiMoveStop();
            _coolTimeFlag = false;
            _coolTime = 0.0f;
        }
        //ray���������Ă��ăG�l�~�[���X�g�A�E�F�|�����X�g����łȂ��Ƃ��C�x���g�����s
        if (_cameraRay.RayHit() && _cllickObjectList._weaponObjList.Count != 0 && _cllickObjectList._enemyObjList.Count != 0 && _coolTimeFlag)
        {
            
            //TODO:�����ɍU���̃C�x���g������
            for (int i = 0; i < _cllickObjectList._weaponObjList.Count; i++)
            {
                _cllickObjectList._weaponObjList[i].GetComponent<WeaponAttack>().Attack(_enemyObj);
            }
            //�^�[�Q�b�gUI�̃A�j���[�V�������Ăяo��
            _targetUiAnimator.SetTrigger(UI_ANIMATOR_TRIGGER_ATTACK);
            _targetUiAnimator.SetBool(UI_ANIMATOR_BOOL_ATTCKHOLD, true);

            _coolTimeFlag = true;
            ObjListClear();
        }

        //if (_coolTimeFlag) 
        //{
        //    _coolTime += Time.deltaTime;
        //}

        if (_coolTime > COOL_TIME_MAX)
        {

            _coolTime = 0;
            _coolTimeFlag = false;
            
        }
    }

    /// <summary>
    /// ���X�g������������֐�
    /// </summary>
    private void ObjListClear()
    {
        
        _cllickObjectList.EnemyListClear();
        _cllickObjectList.WeaponListClear();
        _targetUiAnimator.SetBool(UI_ANIMATOR_BOOL_ATTCKHOLD, false);
    }

    
}
