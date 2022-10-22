using System;
using All.Events;
using Cinemachine;
using Cinemachine.Editor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera m_mainCamera = default;

    // player camera
    [SerializeField] private CinemachineVirtualCamera m_virtualCamera = default;

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
        // m_cameraShakeChannel.OnEventRaised += CameraShake;
    }

    private void OnDisable()
    {
        m_targetChannel.OnEventRaised -= SetTarget;
        // m_cameraShakeChannel.OnEventRaised -= CameraShake;
    }

    #endregion

    private void SetTarget(Transform target)
    {
        m_target = target;
        m_virtualCamera.Follow = target;
        m_virtualCamera.LookAt = target;
    }


    private void CameraShake()
    {
        // TODO: implement me
        throw new NotImplementedException();
    }
}