using System.Collections;
using UnityEngine;
using MRProject.Ray;

public class GunShot : MonoBehaviour, WeaponAttack
{
    [SerializeField]
    private string _weaponName = null;
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private string _fireAnimName;

    RayShot _rayShot = new RayShot();

    [SerializeField]
    private AudioSource _shotAudio = null;
    [SerializeField]
    private GameObject _muzzleFlash = null;
    private ParticleSystem _flash;
    [SerializeField]
    private GameObject _explosionEffect = null;

    [SerializeField]
    private GameObject _rayObj = null;
    private GameObject _hitObj = null;
    [SerializeField]
    private float _rayLength = 10f;

    [SerializeField]
    private int _bulletMax = 5;
    [SerializeField]
    private int _power = 10;
    [SerializeField]
    private int _rate = 10;

    [SerializeField,Header("射撃の間隔")]
    private float _shotCoolTime = 1f;

    private GameObject _targetObj = null;

    private float _step = 0;
    [SerializeField,Header("敵の方向を向く速度")]
    private float _rotationSpeed = 0.5f;
    private Transform _rotationStart;
    private Quaternion _rotationGoal;

    private void Start()
    {
        _targetObj = null;
        _rayShot.Initialize(_rayObj, _rayLength);
        _flash = _muzzleFlash.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _targetObj = null;
        _rayShot.Initialize(_rayObj, _rayLength);
        _flash = _muzzleFlash.GetComponent<ParticleSystem>();
    }

    private void OnDrawGizmos()
    {
        _rayShot.GizmosRayView(_rayObj, _rayLength);
    }

    /// <summary>
    /// 攻撃開始処理
    /// </summary>
    /// <param name="_target">攻撃対象オブジェクト</param>
    public void Attack(GameObject _target)
    {
        if (_target == null) { return; }
        _targetObj = _target;
        
        StartCoroutine(DirectionChange());

    }
    /// <summary>
    /// 攻撃終了処理
    /// </summary>
    public void AttackEnd()
    {
        if (_targetObj.activeSelf == true) { return; }
        _targetObj = null;
    }

    /// <summary>
    /// 敵の方向にゆっくり向く
    /// </summary>
    /// <returns></returns>
    private IEnumerator DirectionChange()
    {
        _step = 0f;
        _rotationStart = this.transform;
        _rotationGoal = Quaternion.LookRotation((_targetObj.transform.position - _rotationStart.position).normalized);
        //敵の方向を向く
        while (this.transform.rotation != _rotationGoal)
        {
            _step += _rotationSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Slerp(_rotationStart.rotation, _rotationGoal, _step);
            yield return null;
        }

        //敵の方向を向いた後に攻撃処理
        _hitObj = _rayShot.ReturnHitObj("Enemy");
        if (_hitObj != null)
        {
            for (int i = 0; i < _bulletMax; i++)
            {
                StartCoroutine(ShotBullet());
                yield return new WaitForSeconds(_shotCoolTime);
            }
        }
        else
        {
            AttackEnd();
        }
        _targetObj = null;

        yield break;
    }

    /// <summary>
    /// 弾丸を発射する処理
    /// </summary>
    private IEnumerator ShotBullet()
    {
        //ヒットしたオブジェクトにダメージ
        _hitObj.GetComponent<EnemyManager>().EnemyHpMinus(_power);

        //発射音
        _shotAudio.Play();
        //発射エフェクト
        _flash.Play();
        //衝突エフェクト

        Vector3 createPos = _rayShot.ReturnHitPos();
        Instantiate(_explosionEffect, createPos, Quaternion.identity);
        //アニメーション再生
        PlayFireAnim();

        yield break;
    }

    /// <summary>
    /// 発射アニメーション再生
    /// </summary>
    private void PlayFireAnim()
    {
        //Animatorを取得できていないとき、処理アニメーションを再生しない
        if (_anim == null) { return; }
        //発射アニメーション
        _anim.Play(_fireAnimName);
    }

    public string ReturnWeaponName()
    {
        return _weaponName;
    }
    public (int,int) ReturnWeaponStatus()
    {
        return (_power,_rate);
    }

}
