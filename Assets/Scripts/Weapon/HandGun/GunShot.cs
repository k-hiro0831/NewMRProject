using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MRProject.Ray;

public class GunShot : MonoBehaviour, WeaponAttack
{
    RayShot _rayShot = new RayShot();

    [SerializeField]
    private GameObject _rayObj = null;
    [SerializeField]
    private float _rayLength = 10f;

    private GameObject _hitObj = null;


    [SerializeField]
    private float _shotCoolTime = 1f;
    private bool _isCoolTime = false;
    private bool _isAttack = false;

    [SerializeField]
    private Animator _anim;

    private GameObject _targetObj = null;

    private void Start()
    {
        _rayShot.Initialize(_rayObj, _rayLength);
    }

    private void Update()
    {
        _hitObj = _rayShot.ReturnHitObj("Enemy");
        if (_hitObj != null)
        {
            //攻撃できるようになる
            AttackPermission();
        }
        else
        {

        }
    }

    private void OnDrawGizmos()
    {
        _rayShot.GizmosRayView(_rayObj, _rayLength);
    }

    /// <summary>
    /// 攻撃可能状態にする
    /// </summary>
    private void AttackPermission()
    {
        //攻撃可能になる
        _isAttack = true;
    }

    public void Attack(GameObject _target)
    {
        //ShotBullet();


        _targetObj = _target;


        Debug.Log("銃で攻撃");
    }
    public void AttackEnd()
    {
        _isAttack = false;
        Debug.Log("銃で攻撃を終了");
    }

    /// <summary>
    /// 弾丸を発射する処理
    /// </summary>
    private void ShotBullet()
    {
        //攻撃中でないとき処理終了
        if (!_isAttack) { return; }
        //クールタイム中、処理終了
        if (_isCoolTime) { return; }

        //ヒットしたオブジェクトにダメージ
        //_hitObj.GetComponent<EnemyDestroy>().EnemyDes();
        Debug.Log("当たりました");

        //<--ヒットエフェクトを生成

        PlayFireAnim();

        //クールタイム
        StartCoroutine(ShotCoolTIme());
    }

    /// <summary>
    /// 発射アニメーション再生
    /// </summary>
    private void PlayFireAnim()
    {
        //Animatorを取得できていないとき、処理アニメーションを再生しない
        if (_anim == null) { return; }
        //発射アニメーション
        _anim.Play("HundGun_Fire");
    }

    /// <summary>
    /// クールタイム処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShotCoolTIme()
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(_shotCoolTime);
        _isCoolTime = false;
        yield break;
    }
}
