using UnityEngine;

public class AttackManager : MonoBehaviour
{
    
    private CllickObjectList _cllickObjectList = default;

    private CameraRay _cameraRay = default;

    private GameObject _enemyObj = default;

    [SerializeField]
    private TargetUIMove _targetUIMove = default;

    [SerializeField,Header("ターゲットUIのアニメーター")]
    private Animator _targetUiAnimator = default;

    //-------------------------------------------------------------------------------------------------------------
    //変数
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
    /// 攻撃イベントを呼び出す関数
    /// </summary>
    public void AttackEvent()
    {
        //TODO:ここに攻撃のイベントを入れる
        for (int i = 0; i < _cllickObjectList._weaponObjList.Count; i++)
        {
            //TODO:_enemyObjをthis.gameobjectに変更
            _cllickObjectList._weaponObjList[i].GetComponent<WeaponAttack>().Attack(_enemyObj);
        }
        //TODO:ターゲットアニメーション関係
        ////ターゲットUIのアニメーションを呼び出す
        //_targetUiAnimator.SetTrigger(UI_ANIMATOR_TRIGGER_ATTACK);
        //_targetUiAnimator.SetBool(UI_ANIMATOR_BOOL_ATTCKHOLD, true);
        ObjListClear();
        

    }

    /// <summary>
    /// リストを初期化する関数
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
