using UnityEngine;

public class ShotRay : MonoBehaviour
{
    private RaycastHit _hit;
    private bool _isHit = false;
    private GameObject _hitObj = null;

    [SerializeField]
    private float _rayLength = 10f;
    [SerializeField]
    private GameObject _rayObj = null;

    private Vector3 _thisPos = Vector3.zero;

    //実行中か判別するフラグ
    private bool _isPlaying = false;

    private void Start()
    {
        _isPlaying = true;
        _isHit = false;
    }

    private void Update()
    {
        _thisPos = _rayObj.transform.position;

        //敵にぶつかっていればTrue、そうでないならFalse
        if (Physics.Raycast(_thisPos, _rayObj.transform.forward, out _hit, _rayLength, LayerMask.GetMask("Enemy")))
        {
            _isHit = true;
            _hitObj = _hit.collider.gameObject.gameObject;
        }
        else
        {
            _isHit = false;
            _hitObj = null;
        }
    }

    /// <summary>
    /// ヒット判定フラグを返す
    /// </summary>
    /// <returns>ヒットフラグ</returns>
    public bool ReturnIsHit()
    {
        return _isHit;
    }
    /// <summary>
    /// ヒット情報を返す
    /// </summary>
    /// <returns></returns>
    public RaycastHit ReturnRayCastHit()
    {
        return _hit;
    }


    private void OnDrawGizmos()
    {
        //実行中でないときは処理する
        //処理の重複を避けるための処理
        if (!_isPlaying)
        {
            _thisPos = _rayObj.transform.position;
            _isHit = Physics.Raycast(_thisPos, _rayObj.transform.forward, out _hit,_rayLength);
        }

        //レイが当たっている間、当たっている位置にボックスを表示
        if (_isHit)
        {
            //線を衝突位置まで表示
            Gizmos.DrawRay(_thisPos, _rayObj.transform.forward * _hit.distance);
        }
        else
        {
            //線を指定した長さ表示
            Gizmos.DrawRay(_thisPos, _rayObj.transform.forward * _rayLength);
        }
    }
}
