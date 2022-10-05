using System;
using All.Events;
using Cinemachine;
using Cinemachine.Editor;
using UnityEngine;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO m_cameraShakeChannel = default;
        [SerializeField] private TransformEventChannel m_targetChannel = default;
        [SerializeField] private CinemachineImpulseSource m_impulseSource;
        // [SerializeField] 
        // TODO: ...
        [SerializeField] private Transform m_target = default;


        #region enable-disable

        private void OnEnable()
        {
            m_targetChannel.OnEventRaised += SetTarget;
            m_cameraShakeChannel.OnEventRaised += CameraShake;
        }

        private void OnDisable()
        {
            m_targetChannel.OnEventRaised -= SetTarget;
            m_cameraShakeChannel.OnEventRaised -= CameraShake;
        }

        #endregion

        private void SetTarget(Transform target)
        {
            m_target = target;
        }


        private void CameraShake()
        {
            // TODO: implement me
            throw new NotImplementedException();
        }
    }
}