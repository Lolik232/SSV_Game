using All.Events;

using UnityEngine;

namespace All.Gameplay
{
	public class PlayerSpawnManager : MonoBehaviour
	{
		[SerializeField] private EntityBase m_playerPrefab = default;

		[Header("Broadcasting")]
		[SerializeField] private VoidEventChannelSO m_onSceneReadyChan = default;
		[SerializeField] private TransformEventChannel m_transformEventChannel = default;
		[SerializeField] private Transform m_defaultSpawnPoint = default;

		private void Awake()
		{
			m_defaultSpawnPoint = transform.GetChild(0);
			if (m_defaultSpawnPoint == null)
			{
				m_defaultSpawnPoint = transform;
			}
		}

		private void OnEnable()
		{
			m_onSceneReadyChan.OnEventRaised += SpawnPlayer;
		}

		private void OnDisable()
		{
			m_onSceneReadyChan.OnEventRaised -= SpawnPlayer;
		}

		private void SpawnPlayer()
		{
			Transform spawnLocation = m_defaultSpawnPoint;
			EntityBase playerInstance = Instantiate(m_playerPrefab, spawnLocation.position, spawnLocation.rotation);

			m_transformEventChannel.RaiseEvent(playerInstance.transform);

			//TODO: send message to PlayerSpawnedChannel for enable input, UI etc.
		}
	}
}