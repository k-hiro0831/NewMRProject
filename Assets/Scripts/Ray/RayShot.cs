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

        //���s�������ʂ���t���O
        private bool _isPlaying = false;

        /// <summary>
        /// �ϐ��̏�����
        /// </summary>
        /// <param name="_centerObj">���C���΂��I�u�W�F�N�g</param>
        /// <param name="_length">���C�̒���</param>
        public void Initialize(GameObject _centerObj, float _length)
        {
            _isHit = false;
            _isPlaying = false;

            _rayObj = _centerObj;
            _rayLength = _length;
            _thisPos = _rayObj.transform.position;
        }

        /// <summary>
        /// ��΂������C�ɏՓ˂����I�u�W�F�N�g��Ԃ�
        /// </summary>
        /// <param name="_targetLayerName">�������肽�����C���[��</param>
        /// <returns>���������ꍇ���̃I�u�W�F�N�g�A�����łȂ��ꍇNULL</returns>
        public GameObject ReturnHitObj(string _targetLayerName)
        {
            GameObject _hitObj = null;
            _thisPos = _rayObj.transform.position;
            //�G�ɂԂ����Ă����True�A�����łȂ��Ȃ�False
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
        /// �M�Y���Ń��C��\������
        /// </summary>
        /// <param name="_centerObj">���C���΂��I�u�W�F�N�g</param>
        /// <param name="_length">���C�̒���</param>
        public void GizmosRayView(GameObject _centerObj, float _length)
        {
            //���s���łȂ��Ƃ��͏�������
            //�����̏d��������邽�߂̏���
            if (!_isPlaying)
            {
                _thisPos = _centerObj.transform.position;
                _isHit = Physics.Raycast(_thisPos, _centerObj.transform.forward, out _hit, _length);
            }

            //���C���������Ă���ԁA�������Ă���ʒu�Ƀ{�b�N�X��\��
            if (_isHit)
            {
                //�����Փˈʒu�܂ŕ\��
                Gizmos.DrawRay(_thisPos, _centerObj.transform.forward * _hit.distance);
            }
            else
            {
                //�����w�肵�������\��
                Gizmos.DrawRay(_thisPos, _centerObj.transform.forward * _length);
            }
        }
    }
}
