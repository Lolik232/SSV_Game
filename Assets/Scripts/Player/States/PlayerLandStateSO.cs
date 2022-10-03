using System.Collections;
using System.Collections.Generic;

using All.Events;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLandState", menuName = "State Machine/States/Player/Sub States/Land")]

public class PlayerLandStateSO : PlayerGroundedStateSO
{
    [SerializeField] private PlayerMoveStateSO _toMoveState;
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerCrouchIdleStateSO _toCrouchIdleState;

    private bool _isLandFinished;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => _isLandFinished));
        transitions.Add(new TransitionItem(_toCrouchIdleState, () => Player.moveInput.y < 0));
        transitions.Add(new TransitionItem(_toMoveState, () => Player.moveInput.x != 0));

        enterActions.Add(() =>
        {
            Player.SetVelocityX(0f);
            _isLandFinished = false;
        });

        animationFinishActions.Add(() => { _isLandFinished = true; });
    }
}
