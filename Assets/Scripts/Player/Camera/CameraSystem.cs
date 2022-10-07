using System;
using System.Collections;
using System.Collections.Generic;
using All.Events;
using UnityEngine;

/// <summary>
/// Управляет состоянием камеры
/// Камера на локации может:
/// 1. Двигаться за персонажем
/// 2. Быть привязанной к конкретной точке
/// </summary>
public class CameraSystem : MonoBehaviour
{
    [Header("Listening to")]
    [SerializeField] private TransformEventChannel m_playerTransformEventChannel = default;


    [Header("Broadcating")]
    [SerializeField] private TransformEventChannel m_targetEventChannel = default;

    private Transform m_playerTransform = default;
    private Transform m_currentPointTransform = null;

    private Transform CurrentPointTransform
    {
        get => m_currentPointTransform;
        set
        {
            m_currentPointTransform = value;
            m_targetEventChannel.RaiseEvent(value);
        }
    }

    public void EnterPivot(Transform pivot)
    {
        m_currentPointTransform = pivot;
        m_targetEventChannel.RaiseEvent(pivot);
    }

    public void ExitPivot(Transform pivot)
    {
        if (CurrentPointTransform == pivot)
        {
            m_targetEventChannel.RaiseEvent(m_playerTransform);
            m_currentPointTransform = null;
        }
    }

    private void OnPlayerTransform(Transform playerTransform)
    {
        m_playerTransform = playerTransform;
        CurrentPointTransform = playerTransform;
    }

    private void OnEnable()
    {
        if (m_playerTransformEventChannel != null)
            m_playerTransformEventChannel.OnEventRaised += OnPlayerTransform;

        m_currentPointTransform = m_playerTransform;
    }

    private void OnDisable()
    {
        if (m_playerTransformEventChannel != null)
            m_playerTransformEventChannel.OnEventRaised -= OnPlayerTransform;
    }
}