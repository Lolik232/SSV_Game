using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]

public class PlayerMoveController : MoveController
{
    public PlayerInputHandler PlayerInputHandler { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
    }
}
