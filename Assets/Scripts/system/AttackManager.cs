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
    private void AttackEvent()
    {
        //rayが当たっているか判定
        if (_cameraRay.RayHit())
        {
            //敵オブジェクト取得
            _enemyObj = _cameraRay.GetHitEnemyObj();
            //敵オブジェクトをリストに追加
            _cllickObjectList.EnemyObjSet(_enemyObj);
            if (!_uiFlag)
            {
                _targetUIMove.UiMoveStart();
                _uiFlag = true;
            }
            _targetUIMove.UiPosUpdate();

            _coolTime += Time.deltaTime;
            
            //クールタイムがマックスより大きければflagをtrueにする
            if (_coolTime > COOL_TIME_MAX)
            {
                _coolTimeFlag = true;
            }

        }
        else
        {
            //ロックが外れた時の初期化
            _uiFlag = false;
            _targetUIMove.UiMoveStop();
            _coolTimeFlag = false;
            _coolTime = 0.0f;
        }
        //rayが当たっていてエネミーリスト、ウェポンリストが空でないときイベントを実行
        if (_cameraRay.RayHit() && _cllickObjectList._weaponObjList.Count != 0 && _cllickObjectList._enemyObjList.Count != 0 && _coolTimeFlag)
        {
            
            //TODO:ここに攻撃のイベントを入れる
            for (int i = 0; i < _cllickObjectList._weaponObjList.Count; i++)
            {
                _cllickObjectList._weaponObjList[i].GetComponent<WeaponAttack>().Attack(_enemyObj);
            }
            //ターゲットUIのアニメーションを呼び出す
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
    /// リストを初期化する関数
    /// </summary>
    private void ObjListClear()
    {
        
        _cllickObjectList.EnemyListClear();
        _cllickObjectList.WeaponListClear();
        _targetUiAnimator.SetBool(UI_ANIMATOR_BOOL_ATTCKHOLD, false);
    }

    
}
