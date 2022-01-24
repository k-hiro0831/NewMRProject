using System.Collections;
using UnityEngine;
using MRProject.Ray;

public class GunShot : MonoBehaviour, WeaponAttack
{
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private string _fireAnimName;

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
    private int _bulletMax = 5;
    [SerializeField]
    private int _bulletCount = 0;
    [SerializeField]
    private int _damage = 10;


    [SerializeField,Header("�ˌ��̊Ԋu")]
    private float _shotCoolTime = 1f;
    private bool _isCoolTime = false;
    private bool _isAttack = false;

    private GameObject _targetObj = null;

    private float _step = 0;
    [SerializeField,Header("�G�̕������������x")]
    private float _rotationSpeed = 0.5f;
    private Transform _rotationStart;

    [SerializeField]
    private GameObject _testTarget;

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
            //this.transform.LookAt(_targetObj.transform);

            StartCoroutine(DirectionChange());
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(_testTarget);
        }
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
        _bulletCount = _bulletMax;
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

        //���߂�ꂽ�e�����˂���
        _bulletCount--;
        if (_bulletCount < 0)
        {
            _bulletCount = 0;
            Debug.Log("�ˌ����I��");
            _isAttack = false;
            _targetObj = null;

            //�����I��
            return;
        }

        //�q�b�g�����I�u�W�F�N�g�Ƀ_���[�W
        //_hitObj.GetComponent<EnemyDestroy>().EnemyDes();

        //<--���˃G�t�F�N�g
        _particle.Play();

        //<--�Փ˃G�t�F�N�g

        PlayFireAnim();
        //�N�[���^�C��
        StartCoroutine(ShotCoolTIme());
    }

    /// <summary>
    /// �G�̕����ɂ���������
    /// </summary>
    /// <returns></returns>
    private IEnumerator DirectionChange()
    {
        _step = 0f;
        _rotationStart = this.transform;

        while(_isAttack)
        {
            _step += _rotationSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Slerp(_rotationStart.rotation, Quaternion.LookRotation
                ((_targetObj.transform.position - _rotationStart.position).normalized), _step);
            yield return null;
        }


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
        _anim.Play(_fireAnimName);
        Debug.Log("animation");
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
