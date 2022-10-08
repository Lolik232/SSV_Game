using System;
using All.Events;
using UnityEngine;

public class CameraPivotAttacher : MonoBehaviour
{
    [Header("Listen to")]
    [SerializeField] private TransformEventChannel m_playerInstantiatedChannel = default;
    [SerializeField] private TransformEventChannel m_enterPivotChannel = default;
    [SerializeField] private TransformEventChannel m_exitPivotChannel  = default;

    [Header("Broadcasting")]
    [SerializeField] private TransformEventChannel m_cameraTargetChannel = default;

    private Transform m_currentCameraTarget = default;
    private Transform m_playerTransform     = default;


    private Transform CurrentCameraTarget
    {
        get => m_currentCameraTarget;
        set
        {
            m_currentCameraTarget = value;
            m_cameraTargetChannel.RaiseEvent(value);
        }
    }

    private void OnEnable()
    {
        m_playerInstantiatedChannel.OnEventRaised += OnPlayerInstantiated;
        m_enterPivotChannel.OnEventRaised         += OnEnterPivot;
        m_exitPivotChannel.OnEventRaised          += OnExitPivot;
    }

    private void OnDisable()
    {
        m_playerInstantiatedChannel.OnEventRaised -= OnPlayerInstantiated;
        m_enterPivotChannel.OnEventRaised         -= OnEnterPivot;
        m_exitPivotChannel.OnEventRaised          -= OnExitPivot;
    }

    private void OnPlayerInstantiated(Transform playerTransform)
    {
        m_playerTransform   = playerTransform;
        CurrentCameraTarget = playerTransform; // only when location loaded
    }

    private void OnEnterPivot(Transform pivot)
    {
        CurrentCameraTarget = pivot;
    }

    private void OnExitPivot(Transform pivot)
    {
        if (CurrentCameraTarget != pivot)
        {
            return;
        }

        CurrentCameraTarget = m_playerTransform;
    }
}