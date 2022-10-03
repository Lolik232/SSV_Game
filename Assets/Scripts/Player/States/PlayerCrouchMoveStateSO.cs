using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchMoveState", menuName = "State Machine/States/Player/Sub States/Crouch Move")]

public class PlayerCrouchMoveStateSO : PlayerGroundedStateSO
{
    [SerializeField] private PlayerMoveStateSO _toMoveState;
    [SerializeField] private PlayerCrouchIdleStateSO _toCrouchIdleState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toMoveState, () => Player.moveInput.y > -1 && !Player.isTouchingCeiling));
        transitions.Add(new TransitionItem(_toCrouchIdleState, () => Player.moveInput.x == 0));

        enterActions.Add(() =>
        {
            Player.SetColiderCrouchSize();
        });

        updateActions.Add(() =>
        {
            Player.CheckIfShouldFlip(Player.moveInput.x);
            Player.SetVelocityX(Player.moveInput.x * Player.CrouchMoveSpeed);
        });

        exitActions.Add(() =>
        {
            Player.SetColiderStandSize();
        });
    }
}
