using System.Collections;
using UnityEngine;
using MRProject.Ray;

public class GunShot : MonoBehaviour, WeaponAttack
{
    [SerializeField]
    private Animator _anim;

    RayShot _rayShot = new RayShot();
    [SerializeField]
    private GameObject _muzzleFlash = null;
    private ParticleSystem _particle;

    [SerializeField]
    private GameObject _rayObj = null;
    private GameObject _hitObj = null;
    [SerializeField]
    private float _rayLength = 10f;

    [SerializeField]
    private int _bulletMax = 30;
    [SerializeField]
    private int _bulletCount = 0;

    [SerializeField]
    private float _shotCoolTime = 1f;
    private bool _isCoolTime = false;
    private bool _isAttack = false;

    private GameObject _targetObj = null;

    [SerializeField]
    private GameObject testTarget;

    private void Start()
    {
        _bulletCount = _bulletMax;
        _isAttack = false; 
        _targetObj = null;
        _rayShot.Initialize(_rayObj, _rayLength);
        _particle = _muzzleFlash.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _bulletCount = _bulletMax;
        _isAttack = false; 
        _targetObj = null;
        _rayShot.Initialize(_rayObj, _rayLength);
        _particle = _muzzleFlash.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (_isAttack)
        {
            this.transform.LookAt(_targetObj.transform);
            _hitObj = _rayShot.ReturnHitObj("Enemy");
            if (_hitObj != null)
            {
                ShotBullet();
            }
            else
            {
                AttackEnd();
            }
        }

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    StartCoroutine(DirectionChange());
        //}
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
        _isAttack = true;
    }
    /// <summary>
    /// 攻撃終了処理
    /// </summary>
    public void AttackEnd()
    {
        if (_targetObj.activeSelf == true) { return; }
        _isAttack = false;
        _targetObj = null;
    }

    /// <summary>
    /// 弾丸を発射する処理
    /// </summary>
    private void ShotBullet()
    {
        //クールタイム中、処理終了
        if (_isCoolTime) { return; }

        _bulletCount--;
        if (_bulletCount <= 0)
        {
            _bulletCount = 0;
            Debug.Log("弾切れ");
            _isAttack = false;
            _targetObj = null;

            //銃を破壊する処理
            this.gameObject.SetActive(false);

            //処理終了
            return;
        }

        //ヒットしたオブジェクトにダメージ
        _hitObj.GetComponent<EnemyDestroy>().EnemyDes();

        //<--発射エフェクト
        _particle.Play();

        //<--衝突エフェクト

        PlayFireAnim();
        //クールタイム
        StartCoroutine(ShotCoolTIme());
    }

    private IEnumerator DirectionChange()
    {

        //Quaternion rotation = Quaternion.LookRotation(testTarget);


        //while (this.transform.rotation != testTarget.transform.rotation)
        //{
        //    float step = 1 * Time.deltaTime;
        //    Quaternion rotation = Quaternion.RotateTowards(this.transform.rotation, testTarget.transform.rotation, step);
        //    this.transform.rotation = rotation;
        //    Debug.Log("aaaaa");
        //    yield return null;
        //}

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
