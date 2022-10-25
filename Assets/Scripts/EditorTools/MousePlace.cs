using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[AddComponentMenu("LostInDarkness/Tools/Mouse Place")]
public class MousePlace : MonoBehaviour
{
    [FormerlySerializedAs("m_isTargeting")]
    [SerializeField] private bool _isTargeting = false;

    public bool IsTargeting { get => _isTargeting; private set => _isTargeting = value; }

    [FormerlySerializedAs("m_targetPosition")]
    [SerializeField] private Vector3 _targetPosition;

    private void OnDrawGizmos()
    {
        if (IsTargeting)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(_targetPosition, Vector2.one * 0.3f);
        }
    }

    public void BeginTargeting()
    {
        IsTargeting      = true;
        _targetPosition = transform.position;
    }

    public void UpdateTargeting(Vector2 spawnPosition)
    {
        _targetPosition.x = spawnPosition.x;
        _targetPosition.y = spawnPosition.y;
    }

    public void EndTargeting()
    {
        IsTargeting = false;
#if UNITY_EDITOR
        Undo.RecordObject(transform, $"{gameObject.name} spawn by Mouse Place");
#endif
        transform.position = _targetPosition;
    }

    public void Cancel()
    {
        IsTargeting      = false;
        _targetPosition = transform.position;
    }
}