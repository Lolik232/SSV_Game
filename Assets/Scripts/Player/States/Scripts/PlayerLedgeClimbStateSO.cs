using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeClimbState", menuName = "Player/States/Ledge Climb")]

public class PlayerLedgeClimbStateSO : PlayerStateSO
{
    private bool _isAnimationFinished;

    [Header("State Transitions")]
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerCrouchIdleStateSO _toCrouchIdleState;

    [Header("Dependent Abilities")]
    [SerializeField] private PlayerJumpAbilitySO _jumpAbility;
    [SerializeField] private PlayerWallJumpAbilitySO _wallJumpAbility;
    [SerializeField] private PlayerDashAbilitySO _dashAbility;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => _isAnimationFinished && !Player.isTouchingCeilingWhenClimb));
        transitions.Add(new TransitionItem(_toCrouchIdleState, () => _isAnimationFinished && Player.isTouchingCeilingWhenClimb));

        enterActions.Add(() =>
        {
            _dashAbility.Cache();
            _dashAbility.Block();
            _jumpAbility.Block();
            _wallJumpAbility.Block();
            Player.SetVelocityZero();
            _isAnimationFinished = false;    
        });

        updateActions.Add(()=>
        {
            Player.HoldPosition(Player.ledgeStartPosition);
        });

        exitActions.Add(() =>
        {
            _dashAbility.Restore();
            Player.transform.position = Player.ledgeEndPosition;
        });

        animationFinishActions.Add(() =>
        {
            _isAnimationFinished = true;
        });
    }
}
