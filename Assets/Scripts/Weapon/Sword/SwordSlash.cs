using System.Collections;
using UnityEngine;
using MRProject.Ray;

public class SwordSlash : MonoBehaviour, WeaponAttack
{
    RayShot _rayShot = new RayShot();

    [SerializeField]
    private GameObject _rayObj = null;
    private GameObject _hitObj = null;
    [SerializeField]
    private float _rayLength = 10f;

    [SerializeField]
    private float _moveSpeed = 1f;

    private bool _isAttack = false;
    private GameObject _targetObj;

    private void Start()
    {
        _isAttack = false;
        _targetObj = null;
        _rayShot.Initialize(_rayObj, _rayLength);
    }

    private void OnEnable()
    {
        _isAttack = false;
        _targetObj = null;
        _rayShot.Initialize(_rayObj, _rayLength);
    }

    private void Update()
    {
        if (_isAttack)
        {
            this.transform.LookAt(_targetObj.transform);
            _hitObj = _rayShot.ReturnHitObj("Enemy");
            if (_hitObj != null)
            {
                Slash();
            }
            else
            {

            }
        }
    }

    private void OnDrawGizmos()
    {
        _rayShot.GizmosRayView(_rayObj, _rayLength);
    }

    private void Slash()
    {
        //ヒットしたオブジェクトにダメージ
        //_hitObj.GetComponent<EnemyDestroy>().EnemyDes();

        //<--衝突エフェクト

        this.gameObject.SetActive(false);
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
        StartCoroutine(Move(_targetObj));
    }
    /// <summary>
    /// 攻撃終了処理
    /// </summary>
    public void AttackEnd()
    {
        _isAttack = false;
        _targetObj = null;
    }


    private IEnumerator Move(GameObject _target)
    {
        //ターゲットの位置まで移動
        while (this.gameObject.transform.position != _target.transform.position)
        {
            this.transform.LookAt(_target.transform);
            this.gameObject.transform.position = Vector3.MoveTowards(
                this.gameObject.transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);

            if (_target.activeSelf == false)
            {
                //<--向いている方向に飛び続ける

                //<--一定時間または一定距離離れたら消す
                Debug.Log("ターゲットが消えた");
                break;
            }

            yield return null;
        }

        yield break;
    }
}
