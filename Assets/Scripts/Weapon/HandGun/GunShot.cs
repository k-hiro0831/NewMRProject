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
            //�U���ł���悤�ɂȂ�
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
    /// �U���\��Ԃɂ���
    /// </summary>
    private void AttackPermission()
    {
        //�U���\�ɂȂ�
        _isAttack = true;
    }

    public void Attack(GameObject _target)
    {
        //ShotBullet();


        _targetObj = _target;


        Debug.Log("�e�ōU��");
    }
    public void AttackEnd()
    {
        _isAttack = false;
        Debug.Log("�e�ōU�����I��");
    }

    /// <summary>
    /// �e�ۂ𔭎˂��鏈��
    /// </summary>
    private void ShotBullet()
    {
        //�U�����łȂ��Ƃ������I��
        if (!_isAttack) { return; }
        //�N�[���^�C�����A�����I��
        if (_isCoolTime) { return; }

        //�q�b�g�����I�u�W�F�N�g�Ƀ_���[�W
        //_hitObj.GetComponent<EnemyDestroy>().EnemyDes();
        Debug.Log("������܂���");

        //<--�q�b�g�G�t�F�N�g�𐶐�

        PlayFireAnim();

        //�N�[���^�C��
        StartCoroutine(ShotCoolTIme());
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
