using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallJumpState", menuName = "State Machine/States/Player/Ability States/Wall Jump")]

public class PlayerWallJumpStateSO : PlayerAbilityStateSO
{
    private float _wallJumpTimeLimit = 0.1f;

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
            Player.jumpStartTime = startTime;
        });

        updateActions.Add(() =>
        {
            abilityDone = (!Player.isTouchingWall && !Player.isTouchingWallBack) || isWallJumpTimeLimitExceeded();
        });

        checks.Add(() =>
        {
            Player.CheckIfTouchingWall();
            Player.CheckIfTouchingWallBack();
        });
    }

    private bool isWallJumpTimeLimitExceeded()
    {
        return Time.time >= startTime + _wallJumpTimeLimit;
    }
}

