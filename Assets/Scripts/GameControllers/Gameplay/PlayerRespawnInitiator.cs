using System;
using All.Events;
using UnityEngine;

namespace All.Gameplay
{
    public class PlayerRespawnInitiator : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _sceneRestartEventChannelSO;
        
        public void Respawn()
        {
            _sceneRestartEventChannelSO.RaiseEvent();
        }
    }
}