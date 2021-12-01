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

    private void Start()
    {
        _rayScript = _rayObj.GetComponent<ShotRay>();
    }

    private void Update()
    {
        if (_rayScript.ReturnIsHit())
        {
            Debug.Log("���C���q�b�g���Ă���");
            ShotBullet();
        }
        else
        {
            Debug.Log("���C���q�b�g���Ă��Ȃ�");
        }
    }

    /// <summary>
    /// �e�ۂ𔭎˂��鏈��
    /// </summary>
    private void ShotBullet()
    {
        if (_isCoolTime) { return; }

        //�v���n�u����
        GameObject _bullet=Instantiate(_bulletPrefab, _rayObj.transform.position, Quaternion.identity);
        //��΂�

        //�N�[���^�C��
        StartCoroutine(ShotCoolTIme());
    }

    private IEnumerator ShotCoolTIme()
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(_shotCoolTime);
        _isCoolTime = false;

        yield break;
    }
}
