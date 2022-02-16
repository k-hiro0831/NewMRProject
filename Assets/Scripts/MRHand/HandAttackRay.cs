using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;

//attackmanagerと同じオブジェクトに入れる
public class HandAttackRay : MonoBehaviour
{
    //TODO:攻撃判定をとるレイを手から出すスクリプト
    //手首から人差し指の方向でレイを飛ばす
    [SerializeField, Range(0.0f, 90.0f)]
    private float _facingThreshold = 80.0f;

    private Vector3 _attackRayVector3 = default;

    [SerializeField,Range(0.0f,20.0f),Header("攻撃レイの長さ")]
    private float _rayRange = default;

    [SerializeField,Range(0.0f,0.5f),Header("出すレイの半径")]
    private float _radius = default;

    [SerializeField,Range(0.0f,5.0f),Header("攻撃のクールタイム")]
    private float _attackCoolTime = default;

    [SerializeField, Header("右手からRayオブジェクト")]
    private GameObject _rayObjectRight = default;

    [SerializeField, Header("左手からRayオブジェクト")]
    private GameObject _rayObjectLeft = default;

    [SerializeField]
    private Color g_xrayColor = default;

    private float _attackCoolTimeCount = default;

    private RaycastHit _attackRayHit = default;

    private AttackManager _attackManager = default;

    private Vector3 _drawRayStartPosition = default;

    private bool _attackFlag = true;

    private const string TAG_NAME_ENEMY = "Enemy";

    private void Start()
    {
        _attackManager = GetComponent<AttackManager>();
    }

    
    private void Update()
    {
        AttackRay();
    }

    /// <summary>
    /// 手がどっちを向いているかチェックする関数
    /// </summary>
    private bool HandDirectionCheck()
    {
        bool check = false;
        // 右手のみ判定したいので右手を取得
        var jointedHand = HandJointUtils.FindHand(Handedness.Right);
        //手のひらを取得できたときに走る
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm, out MixedRealityPose palmPose))
        {
            //手のひらがどっちを向いているかチェック
            if (Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) < _facingThreshold)
            {
                check = true;
            }
        }
        //手のひらを認識できていない時に走る
        else
        {

        }

        return check;
    }

    /// <summary>
    /// アタックレイを出す関数
    /// </summary>
    private void AttackRay()
    {
        AttackFlagSystem();
        
        // 手のひらが認識できているか
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm,Handedness.Right, out MixedRealityPose palmPose))
        {
            
            //指の先端オブジェクト
            MixedRealityPose indexKnucklePose, wristPose;
            //手首と人差し指の根本を取得
            if(HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, Handedness.Right, out indexKnucklePose) && HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist,Handedness.Right , out wristPose))
            {
                //手首から人差し指の根本に向けてのベクトルを計算
                _attackRayVector3 = (indexKnucklePose.Position - wristPose.Position).normalized;
                _drawRayStartPosition = wristPose.Position;
                //レイを飛ばして当たっているか判定、敵に当たっていた場合に走る
                if (Physics.SphereCast(_drawRayStartPosition, _radius,_attackRayVector3,out _attackRayHit,_rayRange) && 
                    _attackRayHit.collider.gameObject.transform.tag== TAG_NAME_ENEMY)
                {
                    RayHitAttack(_attackRayHit.collider.gameObject);
                }


                _rayObjectRight.SetActive(true);
                //オブジェクト位置を変更
                _rayObjectRight.transform.position = wristPose.Position;
                //オブジェクトの向きを変更
                _rayObjectRight.transform.LookAt(indexKnucklePose.Position);
            }

        }
        else
        {
            _rayObjectRight.SetActive(false);
        }

        
        // 手のひらが認識できているか
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out MixedRealityPose palmPoseLeft))
        {
            
            //指の先端オブジェクト
            MixedRealityPose indexKnucklePoseLeft, wristPoseLeft;
            //手首と人差し指の根本を取得
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, Handedness.Left,out indexKnucklePoseLeft) && HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Left,out wristPoseLeft))
            {
                //手首から人差し指の根本に向けてのベクトルを計算
                _attackRayVector3 = (indexKnucklePoseLeft.Position - wristPoseLeft.Position).normalized;
                _drawRayStartPosition = wristPoseLeft.Position;
                //レイを飛ばして当たっているか判定、敵に当たっていた場合に走る
                if (Physics.SphereCast(_drawRayStartPosition, _radius, _attackRayVector3, out _attackRayHit, _rayRange) &&
                    _attackRayHit.collider.gameObject.transform.tag == TAG_NAME_ENEMY)
                {
                    RayHitAttack(_attackRayHit.collider.gameObject);
                }

                //左手攻撃ray処理
                _rayObjectLeft.SetActive(true);
                //rayオブジェクトの位置変更
                _rayObjectLeft.transform.position = wristPoseLeft.Position;
                //rayオブジェクトの向き変更
                _rayObjectLeft.transform.LookAt(indexKnucklePoseLeft.Position);

            }
        }
        else
        {
            _rayObjectLeft.SetActive(false);
        }
    }

    /// <summary>
    /// 攻撃処理を呼ぶ
    /// </summary>
    /// <param name="enemy">攻撃する対象を入れる</param>
    private void RayHitAttack(GameObject enemy)
    {
        if (_attackFlag)
        {
            _attackManager.AttackEvent(enemy);
            _attackFlag = false;
        }
    }

    /// <summary>
    /// attackFlagがfalseの時にカウントを回してtrueにする
    /// </summary>
    private void AttackFlagSystem()
    {
        if (!_attackFlag)
        {
            _attackCoolTimeCount += Time.deltaTime;
            if (_attackCoolTimeCount > _attackCoolTime)
            {
                _attackFlag = true;
                _attackCoolTimeCount = 0;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if (_attackFlag)
        {
            Gizmos.color = g_xrayColor;
            Gizmos.DrawRay(_drawRayStartPosition, _attackRayVector3 * _rayRange);
            Gizmos.DrawWireSphere(_attackRayHit.point, _radius);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position + transform.forward * _rayRange, _radius);
        }


    }
}
