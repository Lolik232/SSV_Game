using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIdleState", menuName = "State Machine/States/Player/Sub States/Idle")]
public class PlayerIdleStateSO : PlayerGroundedStateSO
{
    [SerializeField] private PlayerMoveStateSO _toMoveState;
    [SerializeField] private PlayerCrouchIdleStateSO _toCrouchIdleState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toCrouchIdleState, () => Player.moveInput.y < 0));
        transitions.Add(new TransitionItem(_toMoveState, () => Player.moveInput.x != 0));

        enterActions.Add(() => { Player.SetVelocityZero(); });
    }
}
