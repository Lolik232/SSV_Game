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

        Player.StatesManager.JumpState.EnterEvent += OnJump;
        Player.StatesManager.WallJumpState.EnterEvent += OnJump;
        Player.StatesManager.LedgeClimbState.IsClimbing.ActiveEvent += OnLedgeClimbStart;
        Player.StatesManager.LedgeClimbState.IsClimbing.InactiveEvent += OnLedgeClimbEnd;
    }

    public void LogicUpdate()
    {
        Player.Anim.SetFloat("yVelocity", Player.MoveController.CurrentVelocity.y);
        Player.Anim.SetFloat("xVelocity", Mathf.Abs(Player.MoveController.CurrentVelocity.x));
    }

    private void OnStateEnter(PlayerState state)
    {
        Player.Anim.SetBool(state.AnimBoolName, true);
    }

    private void OnStateExit(PlayerState state)
    {
        Player.Anim.SetBool(state.AnimBoolName, false);
    }

    private void OnJump()
    {
        Player.Anim.SetBool("isJumping", true);
    }

    public void OnJumpDone()
    {
        Player.Anim.SetBool("isJumping", false);
    }

    public void OnLedgeClimbStart()
    {
        Player.Anim.SetBool("isClimbingLedge", true);
    }

    public void OnLedgeClimbEnd()
    {
        Player.Anim.SetBool("isClimbingLedge", false);
    }
}
