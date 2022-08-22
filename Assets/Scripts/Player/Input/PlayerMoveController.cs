using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]

public class PlayerMoveController : MoveController
{
    public PlayerInputHandler PlayerInputHandler { get; private set; }

    public 

    private void Start()
    {
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        ControlledMoveX = PlayerInputHandler.NormInputX;
        ControlledMoveY = PlayerInputHandler.NormInputY;
    }
}
