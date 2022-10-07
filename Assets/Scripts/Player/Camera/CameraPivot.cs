using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Build.Layout;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CameraPivot : MonoBehaviour
{
    private Transform m_currentPivot = default;
    [SerializeField] private CameraSystem m_cameraSystem = default;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out CameraManager cameraSystem)) { m_cameraSystem.EnterPivot(m_currentPivot); }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out CameraManager cameraSystem)) { m_cameraSystem.EnterPivot(m_currentPivot); }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out CameraManager cameraSystem)) { m_cameraSystem.EnterPivot(m_currentPivot); }
    }
}