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

        //�v�[��������
        //_pool = new ObjectPool();
        //_pool.Pool(_poolParent, _bulletPrefab, _poolObjCount);
    }

    private void Update()
    {
        //�e����o�����C���G�ɂ������Ă���Ƃ�
        if (_rayScript.ReturnIsHit())
        {
            ShotBullet();
        }
    }

    /// <summary>
    /// �e�ۂ𔭎˂��鏈��
    /// </summary>
    private void ShotBullet()
    {
        if (_isCoolTime) { return; }

        //�q�b�g���������擾
        RaycastHit _hit = _rayScript.ReturnRayCastHit();
        GameObject _hitObj = _hit.collider.gameObject.gameObject;

        //<--�q�b�g�����I�u�W�F�N�g�Ƀ_���[�W
        _hitObj.GetComponent<EnemyDestroy>().EnemyDes();
        Debug.Log("�������Ă܂�");

        //<--�q�b�g�G�t�F�N�g�𐶐�

        //�e���𐶐�
        //GameObject _mark = _markScript.ReturnBulletMarkPrefab();
        //_mark.transform.position = _hit.point;

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
