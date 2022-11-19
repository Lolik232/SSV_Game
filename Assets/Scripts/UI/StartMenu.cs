using System;
using All.Events;
using Input;
using UnityEngine;

namespace UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _onSceneReady = default;

        private void OnEnable()
        {
            _onSceneReady.OnEventRaised += EnableInput;
        }

        private void OnDisable()
        {
            _onSceneReady.OnEventRaised -= EnableInput;
        }


        private void EnableInput()
        {
            GameInputSingeltone.GameInput.EnableMenuInput();
        }
    }
}