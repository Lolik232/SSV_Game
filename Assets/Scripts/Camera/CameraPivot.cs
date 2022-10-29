using All.Events;

using UnityEngine;

public class CameraPivot : MonoBehaviour
{
	[Header("Broadcasting")]
	[SerializeField] private TransformEventChannel m_cameraTargetEnterChannel = null;
	[SerializeField] private TransformEventChannel m_cameraTargetExitChannel = null;


	private Transform m_currentPivotTransform = default;

	private void Awake()
	{
		m_currentPivotTransform = GetComponent<Transform>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<EntityBase>() != null)
		{
			m_cameraTargetEnterChannel.RaiseEvent(m_currentPivotTransform);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.GetComponent<EntityBase>() != null)
		{
			m_cameraTargetEnterChannel.RaiseEvent(m_currentPivotTransform);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<EntityBase>() != null)
		{
			m_cameraTargetExitChannel.RaiseEvent(m_currentPivotTransform);
		}
	}
}