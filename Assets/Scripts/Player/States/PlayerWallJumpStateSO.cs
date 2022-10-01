using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallJumpState", menuName = "State Machine/States/Player/Ability States/Wall Jump")]

public class PlayerWallJumpStateSO : PlayerAbilityStateSO
{
    protected override void OnEnable()
    {
        base.OnEnable();

        enterActions.Add(() =>
        {
            isGroundedTransitionBlock = true;
            Player.SetVelocity(Player.WallJumpForce, Player.WallJumpAngle, Player.wallDirection);
            Player.CheckIfShouldFlip(Player.wallDirection);
            Player.jumpInput = false;
            Player.wallJump = true;
            SetBool("jump", true);
        });

        updateActions.Add(() =>
        {
            abilityDone = Player.isTouchingWall == Player.isTouchingWallBack;
        });

        exitActions.Add(() => { SetBool("jump", false); });
    }
}

