using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrouchIdleState", menuName = "Player/States/Grounded/Crouch Idle")]

public class PlayerCrouchIdleStateSO : PlayerGroundedStateSO
{
    [Header("State Transitions")]
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerCrouchMoveStateSO _toCrouchMoveState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => Player.moveInput.y > -1 && !Player.isTouchingCeiling));
        transitions.Add(new TransitionItem(_toCrouchMoveState, () => Player.moveInput.x != 0));

        enterActions.Add(() =>
        {
            Player.TrySetVelocityZero();
            Player.Crouch();
        });

        exitActions.Add(() =>
        {
            Player.Stand();
        });
    }
}
