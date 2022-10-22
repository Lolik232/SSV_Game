using All.Events;

using UnityEngine;

namespace SceneManagement
{
	[RequireComponent(typeof(Collider2D))]
	public class LocationExit : MonoBehaviour
	{
		[SerializeField] private GameSceneSO m_locationToLoad = default;

		[Header("Broadcasting")]
		[SerializeField] private LoadEventChannelSO m_loadLocationChannel = default;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent<EntityBase>(out var entity))
			{
				m_loadLocationChannel.RaiseEvent(m_locationToLoad, false, true);
			}
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.TryGetComponent<EntityBase>(out var entity))
			{
				m_loadLocationChannel.RaiseEvent(m_locationToLoad, false, true);
			}
		}

		// private void OnCollisionEnter2D(Collision2D col)
		// {
		//     throw new NotImplementedException();
		// }
	}
}