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

    private float _step = 0;
    [SerializeField]
    private float _rotationSpeed = 0.5f;
    private Transform _rotationStart;

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
            //this.transform.LookAt(_targetObj.transform);
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
        _hitObj.GetComponent<EnemyManager>().EnemyHpMinus(1);

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
        StartCoroutine(DirectionChange());

        //StartCoroutine(Move());

    }
    /// <summary>
    /// 攻撃終了処理
    /// </summary>
    public void AttackEnd()
    {
        _isAttack = false;
        _targetObj = null;
    }

    /// <summary>
    /// 敵の方向にゆっくり向く
    /// </summary>
    /// <returns></returns>
    private IEnumerator DirectionChange()
    {
        _step = 0f;
        _rotationStart = this.transform;

        //while (_isAttack)
        //{
        //    _step += _rotationSpeed * Time.deltaTime;
        //    this.transform.rotation = Quaternion.Slerp(_rotationStart.rotation, Quaternion.LookRotation
        //        ((_targetObj.transform.position - _rotationStart.position).normalized), _step);
        //    yield return null;
        //}

        for(int i = 0; i < 60; i++)
        {
            _step += _rotationSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Slerp(_rotationStart.rotation, Quaternion.LookRotation
                ((_targetObj.transform.position - _rotationStart.position).normalized), _step);
            yield return null;
        }

        StartCoroutine(Move());
        
        yield break;
    }

    private IEnumerator Move()
    {
        //ターゲットの位置まで移動
        while (this.gameObject.transform.position != _targetObj.transform.position)
        {
            this.transform.LookAt(_targetObj.transform);
            this.gameObject.transform.position = Vector3.MoveTowards(
                this.gameObject.transform.position, _targetObj.transform.position, _moveSpeed * Time.deltaTime);

            if (_targetObj.activeSelf == false)
            {
                //<--向いている方向に飛び続ける

                //<--一定時間または一定距離離れたら消す
                Debug.Log("ターゲットが消えた");

                AttackEnd();

                break;
            }

            yield return null;
        }

        yield break;
    }
}
