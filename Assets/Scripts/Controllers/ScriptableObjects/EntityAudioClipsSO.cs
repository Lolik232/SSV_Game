using UnityEngine;

namespace Controllers.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Entity/Audio", fileName = "Audio Source")]
    public class EntityAudioClipsSO : ScriptableObject
    {
        [SerializeField] private AudioClip _hitSound  = default;
        [SerializeField] private AudioClip _moveSound = default;
        [SerializeField] private AudioClip _landing   = default;
        [SerializeField] private AudioClip _jump;
        
        #region Getters

        public AudioClip HitSound  => _hitSound;
        public AudioClip MoveSound => _moveSound;
        public AudioClip Landing   => _landing;
        public AudioClip Jump      => _jump;

        #endregion
    }
}