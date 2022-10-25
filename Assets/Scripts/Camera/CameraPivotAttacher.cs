using System;

using All.Events;

using UnityEngine;
using UnityEngine.Serialization;

public class CameraPivotAttacher : MonoBehaviour
{
	[Header("Listen to")]
	[FormerlySerializedAs("m_playerInstantiatedChannel")]
	[SerializeField] private TransformEventChannel _playerInstantiatedChannel = default;
	[FormerlySerializedAs("m_enterPivotChannel")]
	[SerializeField] private TransformEventChannel _enterPivotChannel = default;
	[FormerlySerializedAs("m_exitPivotChannel")]
	[SerializeField] private TransformEventChannel _exitPivotChannel = default;

	[Header("Broadcasting")]
	[FormerlySerializedAs("m_cameraTargetChannel")]
	[SerializeField] private TransformEventChannel _cameraTargetChannel = default;

	private Transform m_currentCameraTarget = default;
	private Transform m_playerTransform     = default;


	private Transform CurrentCameraTarget
	{
		get => m_currentCameraTarget;
		set
		{
			m_currentCameraTarget = value;
			_cameraTargetChannel.RaiseEvent(value);
		}
	}

	private void OnEnable()
	{
		_playerInstantiatedChannel.OnEventRaised += OnPlayerInstantiated;
		_enterPivotChannel.OnEventRaised         += OnEnterPivot;
		_exitPivotChannel.OnEventRaised          += OnExitPivot;
	}

	private void OnDisable()
	{
		_playerInstantiatedChannel.OnEventRaised -= OnPlayerInstantiated;
		_enterPivotChannel.OnEventRaised         -= OnEnterPivot;
		_exitPivotChannel.OnEventRaised          -= OnExitPivot;
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