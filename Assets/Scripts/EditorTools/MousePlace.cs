using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[AddComponentMenu("LostInDarkness/Tools/Mouse Place")]
public class MousePlace : MonoBehaviour
{
	[HideInInspector] [SerializeField] private bool m_isTargeting = false;
	public bool IsTargeting
	{
		get => m_isTargeting; private set => m_isTargeting = value;
	}

	[SerializeField] private Vector3 m_targetPosition;

	private void OnDrawGizmos()
	{
		if (IsTargeting)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawCube(m_targetPosition, Vector2.one * 0.3f);
		}
	}

	public void BeginTargeting()
	{
		IsTargeting = true;
		m_targetPosition = transform.position;
	}

	public void UpdateTargeting(Vector2 spawnPosition)
	{
		m_targetPosition.x = spawnPosition.x;
		m_targetPosition.y = spawnPosition.y;
	}

	public void EndTargeting()
	{
		IsTargeting = false;
#if UNITY_EDITOR
		Undo.RecordObject(transform, $"{gameObject.name} spawn by Mouse Place");
#endif
		transform.position = m_targetPosition;
	}

	public void Cancel()
	{
		IsTargeting = false;
		m_targetPosition = transform.position;
	}
}