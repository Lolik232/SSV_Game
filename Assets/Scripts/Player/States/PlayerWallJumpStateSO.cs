using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallJumpState", menuName = "State Machine/States/Player/Ability States/Wall Jump")]

public class PlayerWallJumpStateSO : PlayerAbilityStateSO
{
    [Header("Connected Ability")]
    [SerializeField] private PlayerWallJumpAbilitySO _wallJumpAbility;

    protected override void OnEnable()
    {
        base.OnEnable();

        enterActions.Add(() =>
        {
            isGroundedTransitionBlock = true;
            Player.SetVelocity(_wallJumpAbility.Force, _wallJumpAbility.Angle, Player.wallDirection);
            Player.CheckIfShouldFlip(Player.wallDirection);
        });

        updateActions.Add(() =>
        {
            abilityDone = Player.isTouchingWall == Player.isTouchingWallBack || _wallJumpAbility.NeedHardExit();
        });

        checks.Add(() =>
        {
            Player.CheckIfTouchingWall();
            Player.CheckIfTouchingWallBack();
        });
    }
}