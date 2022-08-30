using System;

using UnityEngine;

public class PlayerAnimationController
{ 
    public readonly Player Player;

    public PlayerAnimationController(Player player)
    {
        Player = player;
    }

    public void Initialize()
    {
        Player.StatesManager.StateMachine.StateEnterEvent += OnStateEnter;
        Player.StatesManager.StateMachine.StateExitEvent += OnStateExit;
    }

    private void OnStateEnter(PlayerState state)
    {
        Player.Anim.SetBool(state.AnimBoolName, true);
    }

    private void OnStateExit(PlayerState state)
    {
        Player.Anim.SetBool(state.AnimBoolName, false);
    }
}
