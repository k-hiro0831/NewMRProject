using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;

namespace SpatialAwarenessNavMesh
{
    using SpatialAwarenessHandler = IMixedRealitySpatialAwarenessObservationHandler<SpatialAwarenessMeshObject>;

    public class SpatialAwarenessNavMesh : MonoBehaviour, SpatialAwarenessHandler
    {
        /// <summary>
        /// Value indicating whether or not this script has registered for spatial awareness events.
        /// ���̃X�N���v�g�� SpatialAwareness �C�x���g�ɓo�^����Ă��邩�ǂ����������l�B
        /// </summary>
        private bool isRegistered = false;

        private void Start()
        {
            RegisterEventHandlers();
        }

        private void OnEnable()
        {
            RegisterEventHandlers();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
        }

        private void OnDestroy()
        {
            UnregisterEventHandlers();
        }

        /// <summary>
        /// Registers for the spatial awareness system events.
        /// </summary>
        private void RegisterEventHandlers()
        {
            if (!isRegistered && (CoreServices.SpatialAwarenessSystem != null))
            {
                CoreServices.SpatialAwarenessSystem.RegisterHandler<SpatialAwarenessHandler>(this);
                isRegistered = true;
            }
        }

        /// <summary>
        /// Unregisters from the spatial awareness system events.
        /// </summary>
        private void UnregisterEventHandlers()
        {
            if (isRegistered && (CoreServices.SpatialAwarenessSystem != null))
            {
                CoreServices.SpatialAwarenessSystem.UnregisterHandler<SpatialAwarenessHandler>(this);
                isRegistered = false;
            }
        }

        /// <inheritdoc />
        public virtual void OnObservationAdded(MixedRealitySpatialAwarenessEventData<SpatialAwarenessMeshObject> eventData)
        {
            // Mesh�ǉ����̃C�x���g
            Debug.Log($"Tracking mesh {eventData.Id}");

            // NavMeshSourceTag ���I�u�W�F�N�g�ɒǉ�
            eventData.SpatialObject.GameObject.AddComponent<NavMeshSourceTag>();
        }

        /// <inheritdoc />
        public virtual void OnObservationUpdated(MixedRealitySpatialAwarenessEventData<SpatialAwarenessMeshObject> eventData)
        {
            // Mesh�X�V���̃C�x���g
            Debug.Log($"Update mesh {eventData.Id}");

            // NavMeshSourceTag ���I�u�W�F�N�g�ɒǉ�
            NavMeshSourceTag tag = eventData.SpatialObject.GameObject.GetComponent<NavMeshSourceTag>();
            if (tag == null)
            {
                eventData.SpatialObject.GameObject.AddComponent<NavMeshSourceTag>();
            }
        }

        /// <inheritdoc />
        public virtual void OnObservationRemoved(MixedRealitySpatialAwarenessEventData<SpatialAwarenessMeshObject> eventData)
        {
            // Mesh�폜���̃C�x���g
            Debug.Log($"Remove mesh {eventData.Id}");
        }
    }
}
