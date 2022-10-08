using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeClimbState", menuName = "State Machine/States/Player/States/Ledge Climb")]

public class PlayerLedgeClimbStateSO : PlayerStateSO
{
    private bool _isAnimationFinished;

    [Header("State Transitions")]
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerCrouchIdleStateSO _toCrouchIdleState;


    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => _isAnimationFinished && !Player.isTouchingCeiling));
        transitions.Add(new TransitionItem(_toCrouchIdleState, () => _isAnimationFinished && Player.isTouchingCeiling));

        enterActions.Add(() =>
        {
            Player.SetVelocityZero();
            Player.CheckIfTouchingCeilingWhenClimb();
            _isAnimationFinished = false;    
        });

        updateActions.Add(()=>
        {
            Player.HoldPosition(Player.ledgeStartPosition);
        });

        exitActions.Add(() =>
        {
            Player.transform.position = Player.ledgeEndPosition;
        });

        animationFinishActions.Add(() =>
        {
            _isAnimationFinished = true;
        });
    }
}
