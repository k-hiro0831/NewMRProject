using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab = null;
    [SerializeField]
    private GameObject _rayObj = null;
    private ShotRay _rayScript;

    [SerializeField]
    private float _shotCoolTime = 1f;
    private bool _isCoolTime = false;

    [SerializeField]
    private Animator _anim;

    private void Start()
    {
        _rayScript = _rayObj.GetComponent<ShotRay>();

        //�v�[��������
        //�l������
    }

    private void Update()
    {
        //Debug.Log(transform.forward);
        if (_rayScript.ReturnIsHit())
        {
            //Debug.Log("���C���q�b�g���Ă���");
            ShotBullet();
        }
        else
        {
            //Debug.Log("���C���q�b�g���Ă��Ȃ�");
        }
    }

    /// <summary>
    /// �e�ۂ𔭎˂��鏈��
    /// </summary>
    private void ShotBullet()
    {
        if (_isCoolTime) { return; }

        //�v�[������e�����o��
        //�e�̕�����ς���

        //�v���n�u����
        GameObject _bullet=Instantiate(_bulletPrefab, _rayObj.transform.position, Quaternion.identity);
        _bullet.GetComponent<HandGumBullet>().ChangeMoveDirection(this.gameObject);

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

    private IEnumerator ShotCoolTIme()
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(_shotCoolTime);
        _isCoolTime = false;

        yield break;
    }
}
