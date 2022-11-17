using All.Events;
using Systems.SaveSystem;
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

        [SerializeField] private SaveSystem _saveSystem = default;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out var entity))
            {
                _saveSystem.SetupSave(_locationToLoad);
                _saveSystem.SaveGameOnDisk();
                
                _loadLocationChannel.RaiseEvent(_locationToLoad, false, true);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent<Player>(out var entity))
            {
                _saveSystem.SetupSave(_locationToLoad);
                _saveSystem.SaveGameOnDisk();
                
                _loadLocationChannel.RaiseEvent(_locationToLoad, false, true);
            }
        }
    }
}