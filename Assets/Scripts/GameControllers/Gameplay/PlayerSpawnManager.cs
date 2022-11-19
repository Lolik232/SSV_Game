using All.Events;
using Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace All.Gameplay
{
    public class PlayerSpawnManager : MonoBehaviour
    {
        [FormerlySerializedAs("m_playerPrefab")]
        [SerializeField] private Player _playerPrefab = default;

        [Header("Broadcasting")]
        [FormerlySerializedAs("m_onSceneReadyChan")]
        [SerializeField] private VoidEventChannelSO _onSceneReadyChan = default;

        [FormerlySerializedAs("m_transformEventChannel")]
        [SerializeField] private TransformEventChannel _transformEventChannel = default;

        [FormerlySerializedAs("m_defaultSpawnPoint")]
        [SerializeField] private Transform _defaultSpawnPoint = default;

        private void Awake()
        {
            _defaultSpawnPoint = transform.GetChild(0);
            if (_defaultSpawnPoint == null)
            {
                _defaultSpawnPoint = transform;
            }
        }

        private void OnEnable()
        {
            _onSceneReadyChan.OnEventRaised += SpawnPlayer;
        }

        private void OnDisable()
        {
            _onSceneReadyChan.OnEventRaised -= SpawnPlayer;
        }

        private void SpawnPlayer()
        {
            Transform spawnLocation  = _defaultSpawnPoint;
            Player    playerInstance = Instantiate(_playerPrefab, spawnLocation.position, spawnLocation.rotation);

            _transformEventChannel.RaiseEvent(playerInstance.transform);

            Debug.Log("Player spawned");

            //TODO: send message to PlayerSpawnedChannel for enable input, UI etc.
            GameInputSingeltone.GameInput.EnableGameplayInput();
        }
    }
}