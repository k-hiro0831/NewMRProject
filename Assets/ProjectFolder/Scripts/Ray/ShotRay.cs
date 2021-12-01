using UnityEngine;

public class ShotRay : MonoBehaviour
{
    RaycastHit _hit;
    [SerializeField]
    private bool _isHit = false;

    [SerializeField]
    private float _rayLength = 10f;
    private Vector3 _thisPos = Vector3.zero;
    private float _thisScale = 1f;

    //実行中か判別するフラグ
    private bool _isPlaying = false;

    private void Start()
    {
        _isPlaying = true;
        _isHit = false;
    }

    private void Update()
    {
        _thisPos = transform.position;

        //敵にぶつかっていればTrue、そうでないならFalse
        if (Physics.Raycast(_thisPos, transform.forward, out _hit, _rayLength, LayerMask.GetMask("Enemy")))
        {
            _isHit = true;
        }
        else
        {
            _isHit = false;
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

    private void OnDrawGizmos()
    {
        //実行中でないときは処理する
        //処理の重複を避けるための処理
        if (!_isPlaying)
        {
            _thisPos = transform.position;
            _isHit = Physics.Raycast(_thisPos, transform.forward, out _hit,_rayLength);
        }

        //レイが当たっている間、当たっている位置にボックスを表示
        if (_isHit)
        {
            //線を衝突位置まで表示
            Gizmos.DrawRay(_thisPos, transform.forward * _hit.distance);
        }
        else
        {
            //線を指定した長さ表示
            Gizmos.DrawRay(_thisPos, transform.forward * _rayLength);
        }
    }
}
