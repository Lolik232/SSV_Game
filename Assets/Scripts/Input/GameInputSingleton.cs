using System;
using UnityEngine;

namespace Input
{
    public static class GameInputSingleton 
    {
        private static GameInput _gameInput;

        public static GameInput GameInput
        {
            get
            {
                _gameInput ??= new GameInput();
                return _gameInput;
            }
        }
    }
}