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
    /// �U���J�n����
    /// </summary>
    /// <param name="_target">�U���ΏۃI�u�W�F�N�g</param>
    public void Attack(GameObject _target)
    {
        if (_target == null) { return; }
        _targetObj = _target;
        _isAttack = true;
    }
    /// <summary>
    /// �U���I������
    /// </summary>
    public void AttackEnd()
    {
        if (_targetObj.activeSelf == true) { return; }
        _isAttack = false;
        _targetObj = null;
    }

    /// <summary>
    /// �e�ۂ𔭎˂��鏈��
    /// </summary>
    private void ShotBullet()
    {
        //�N�[���^�C�����A�����I��
        if (_isCoolTime) { return; }

        _bulletCount--;
        if (_bulletCount <= 0)
        {
            _bulletCount = 0;
            Debug.Log("�e�؂�");
            _isAttack = false;
            _targetObj = null;

            //�e��j�󂷂鏈��
            this.gameObject.SetActive(false);

            //�����I��
            return;
        }

        //�q�b�g�����I�u�W�F�N�g�Ƀ_���[�W
        _hitObj.GetComponent<EnemyDestroy>().EnemyDes();

        //<--���˃G�t�F�N�g
        _particle.Play();

        //<--�Փ˃G�t�F�N�g

        PlayFireAnim();
        //�N�[���^�C��
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
    /// ���˃A�j���[�V�����Đ�
    /// </summary>
    private void PlayFireAnim()
    {
        //Animator���擾�ł��Ă��Ȃ��Ƃ��A�����A�j���[�V�������Đ����Ȃ�
        if (_anim == null) { return; }
        //���˃A�j���[�V����
        _anim.Play("HundGun_Fire");
    }

    /// <summary>
    /// �N�[���^�C������
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
