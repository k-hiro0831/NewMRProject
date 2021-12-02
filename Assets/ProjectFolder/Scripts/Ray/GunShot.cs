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

        //プールをつくる
        //値を入れる
    }

    private void Update()
    {
        //Debug.Log(transform.forward);
        if (_rayScript.ReturnIsHit())
        {
            Debug.Log("レイがヒットしている");
            ShotBullet();
        }
        else
        {
            //Debug.Log("レイがヒットしていない");
        }
    }

    /// <summary>
    /// 弾丸を発射する処理
    /// </summary>
    private void ShotBullet()
    {
        if (_isCoolTime) { return; }

        //プールから弾を取り出す
        //弾の方向を変える

        //プレハブ生成
        GameObject _bullet=Instantiate(_bulletPrefab, _rayObj.transform.position, Quaternion.identity);
        _bullet.GetComponent<Bullet>().ChangeMoveDirection(this.gameObject);

        //クールタイム
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
