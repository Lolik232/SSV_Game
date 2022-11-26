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
            GameInputSingleton.GameInput.EnableMenuInput();
        }
    }
}