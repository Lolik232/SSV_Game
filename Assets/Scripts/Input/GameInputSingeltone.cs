using System;
using UnityEngine;

namespace Input
{
    public static class GameInputSingeltone 
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