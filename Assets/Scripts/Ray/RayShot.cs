using UnityEngine;

namespace MRProject.Ray
{
    class RayShot
    {
        private RaycastHit _hit;
        private bool _isHit = false;

        private GameObject _rayObj = null;
        private float _rayLength = 10f;
        private Vector3 _thisPos = Vector3.zero;

        //実行中か判別するフラグ
        private bool _isPlaying = false;

        /// <summary>
        /// 変数の初期化
        /// </summary>
        /// <param name="_centerObj">レイを飛ばすオブジェクト</param>
        /// <param name="_length">レイの長さ</param>
        public void Initialize(GameObject _centerObj, float _length)
        {
            _isHit = false;
            _isPlaying = false;

            _rayObj = _centerObj;
            _rayLength = _length;
            _thisPos = _rayObj.transform.position;
        }

        /// <summary>
        /// 飛ばしたレイに衝突したオブジェクトを返す
        /// </summary>
        /// <param name="_targetLayerName">判定を取りたいレイヤー名</param>
        /// <returns>当たった場合そのオブジェクト、そうでない場合NULL</returns>
        public GameObject ReturnHitObj(string _targetLayerName)
        {
            GameObject _hitObj = null;
            _thisPos = _rayObj.transform.position;
            //敵にぶつかっていればTrue、そうでないならFalse
            if (Physics.Raycast(_thisPos, _rayObj.transform.forward, out _hit, _rayLength, LayerMask.GetMask(_targetLayerName)))
            {
                _hitObj = _hit.collider.gameObject.gameObject;
            }
            else
            {
                _hitObj = null;
            }

            return _hitObj;
        }

        public Vector3 ReturnHitPos()
        {
            return _hit.point;
        }

        /// <summary>
        /// ギズモでレイを表示する
        /// </summary>
        /// <param name="_centerObj">レイを飛ばすオブジェクト</param>
        /// <param name="_length">レイの長さ</param>
        public void GizmosRayView(GameObject _centerObj, float _length)
        {
            //実行中でないときは処理する
            //処理の重複を避けるための処理
            if (!_isPlaying)
            {
                _thisPos = _centerObj.transform.position;
                _isHit = Physics.Raycast(_thisPos, _centerObj.transform.forward, out _hit, _length);
            }

            //レイが当たっている間、当たっている位置にボックスを表示
            if (_isHit)
            {
                //線を衝突位置まで表示
                Gizmos.DrawRay(_thisPos, _centerObj.transform.forward * _hit.distance);
            }
            else
            {
                //線を指定した長さ表示
                Gizmos.DrawRay(_thisPos, _centerObj.transform.forward * _length);
            }
        }
    }
}
