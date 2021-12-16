using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MRProject.Ray;

public class SwordSlash : MonoBehaviour, WeaponAttack
{
    RayShot _rayShot = new RayShot();

    [SerializeField]
    private GameObject _rayObj = null;
    [SerializeField]
    private float _rayLength = 10f;

    private GameObject _hitObj = null;

    private bool _isAttack = false;

    private void Start()
    {
        _rayShot.Initialize(_rayObj, _rayLength);

    }

    private void Update()
    {
        _hitObj = _rayShot.ReturnHitObj("Enemy");
        if (_hitObj != null)
        {
            Slash();
        }
        else
        {

        }
    }

    private void OnDrawGizmos()
    {
        _rayShot.GizmosRayView(_rayObj, _rayLength);
    }

    private void Slash()
    {
        //攻撃中でないとき処理終了
        if (!_isAttack) { return; }

        //ヒットしたオブジェクトにダメージ
        //_hitObj.GetComponent<EnemyDestroy>().EnemyDes();
        Debug.Log("当たりました");
    }

    public void Attack(GameObject _target)
    {
        _isAttack = true;
        Debug.Log("剣で攻撃");
    }
    public void AttackEnd()
    {
        _isAttack = false;
        Debug.Log("剣で攻撃を終了");
    }
}
