using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab = null;
    [SerializeField]
    private GameObject _poolParent = null;
    [SerializeField]
    private int _poolObjCount = 10;

    //[SerializeField]
    //private GameObject _bulletMarkParent = null;
    //private BulletMarkManager _markScript;

    [SerializeField]
    private GameObject _rayObj = null;
    private ShotRay _rayScript;

    [SerializeField]
    private float _shotCoolTime = 1f;
    private bool _isCoolTime = false;

    [SerializeField]
    private Animator _anim;

    private ObjectPool _pool;

    private void Start()
    {
        _rayScript = GetComponent<ShotRay>();
        //_markScript = _bulletMarkParent.GetComponent<BulletMarkManager>();

        //プールをつくる
        //_pool = new ObjectPool();
        //_pool.Pool(_poolParent, _bulletPrefab, _poolObjCount);
    }

    private void Update()
    {
        //銃から出すレイが敵にあたっているとき
        if (_rayScript.ReturnIsHit())
        {
            ShotBullet();
        }
    }

    /// <summary>
    /// 弾丸を発射する処理
    /// </summary>
    private void ShotBullet()
    {
        if (_isCoolTime) { return; }

        //ヒットした情報を取得
        RaycastHit _hit = _rayScript.ReturnRayCastHit();
        GameObject _hitObj = _hit.collider.gameObject.gameObject;

        //<--ヒットしたオブジェクトにダメージ
        _hitObj.GetComponent<EnemyDestroy>().EnemyDes();
        Debug.Log("当たってます");

        //<--ヒットエフェクトを生成

        //銃痕を生成
        //GameObject _mark = _markScript.ReturnBulletMarkPrefab();
        //_mark.transform.position = _hit.point;

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
