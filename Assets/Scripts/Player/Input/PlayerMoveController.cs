using System;
using UnityEngine;

public class PlayerMoveController : MoveController
{
    public PlayerInputHandler InputHandler { get; private set; }

    public PlayerMoveController(Transform transform, Rigidbody2D rigidbody2D, PlayerInputHandler inputHandler) : base(transform, rigidbody2D)
    {
        InputHandler = inputHandler;
    }
}
