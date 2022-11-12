using All.Events;

using UnityEngine;
using UnityEngine.Serialization;

namespace SceneManagement
{
	[RequireComponent(typeof(Collider2D))]
	public class LocationExit : MonoBehaviour
	{
		[FormerlySerializedAs("m_locationToLoad")]
		[SerializeField] private GameSceneSO _locationToLoad = default;

		[Header("Broadcasting")]
		[FormerlySerializedAs("m_loadLocationChannel")]
		[SerializeField] private LoadEventChannelSO _loadLocationChannel = default;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent<Player>(out var entity))
			{
				_loadLocationChannel.RaiseEvent(_locationToLoad, false, true);
			}
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.TryGetComponent<Player>(out var entity))
			{
				_loadLocationChannel.RaiseEvent(_locationToLoad, false, true);
			}
		}
	}
}