using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]

public class PlayerMoveController : MoveController
{
    public PlayerInputHandler PlayerInputHandler { get; private set; }

    private void Start()
    {
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
    }
}
