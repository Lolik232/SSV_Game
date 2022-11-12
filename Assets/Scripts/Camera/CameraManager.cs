using System;

using All.Events;

using Cinemachine;
using Cinemachine.Editor;

using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
	[FormerlySerializedAs("m_mainCamera")]
	[SerializeField] private Camera _mainCamera = default;

	// entity camera
	[FormerlySerializedAs("m_virtualCamera")]
	[SerializeField] private CinemachineVirtualCamera _virtualCamera = default;

	[FormerlySerializedAs("m_cameraShakeChannel")]
	[SerializeField] private VoidEventChannelSO _cameraShakeChannel = default;

	[FormerlySerializedAs("m_targetChannel")]
	[SerializeField] private TransformEventChannel _targetChannel = default;

	[FormerlySerializedAs("m_impulseSource")]
	[SerializeField] private CinemachineImpulseSource _impulseSource;

	// [SerializeField] 
	// TODO: ...
	[FormerlySerializedAs("m_target")]
	[SerializeField] private Transform _target = default;


	#region enable-disable

	private void OnEnable()
	{
		_targetChannel.OnEventRaised += SetTarget;
		// m_cameraShakeChannel.OnEventRaised += CameraShake;
	}

	private void OnDisable()
	{
		_targetChannel.OnEventRaised -= SetTarget;
		// m_cameraShakeChannel.OnEventRaised -= CameraShake;
	}

	#endregion

	private void SetTarget(Transform target)
	{
		_target               = target;
		_virtualCamera.Follow = target;
		_virtualCamera.LookAt = target;
	}


	private void CameraShake()
	{
		// TODO: implement me
		throw new NotImplementedException();
	}
}