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
    [Header("Listening to")] [SerializeField]
    private TransformEventChannel m_playerTransformEventChannel = default;


    [Header("Broadcating")] [SerializeField]
    private TransformEventChannel m_targetEventChannel = default;

    private Transform m_playerTransform = default;
    private Transform m_currentPointTransform = default;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
}