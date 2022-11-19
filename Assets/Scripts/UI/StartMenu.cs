using System;
using Input;
using UnityEngine;

namespace UI
{
    public class StartMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            GameInputSingeltone.GameInput.EnableMenuInput();
        }
    }
}