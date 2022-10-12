using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeHoldState", menuName = "Player/States/Ledge Hold")]

public class PlayerLedgeHoldStateSO : PlayerStateSO
{
    private bool _isHanging;

    [Header("State Transitions")]
    [SerializeField] private PlayerInAirStateSO _toInAirState;
    [SerializeField] private PlayerLedgeClimbStateSO _toLedgeClimbState;

    [Header("Dependent Abilities")]
    [SerializeField] private PlayerJumpAbilitySO _jumpAbility;
    [SerializeField] private PlayerWallJumpAbilitySO _wallJumpAbility;
    [SerializeField] private PlayerDashAbilitySO _dashAbility;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toLedgeClimbState, () => _isHanging && (Player.moveInput.x == Player.facingDirection || Player.moveInput.y == 1)));
        transitions.Add(new TransitionItem(_toInAirState, () => _isHanging && (_wallJumpAbility.isActive || Player.moveInput.x == -Player.facingDirection || Player.moveInput.y == -1)));

        enterActions.Add(() =>
        {
            _isHanging = false;
            _dashAbility.Block();
            _jumpAbility.SetZeroAmountOfUsages();
            _wallJumpAbility.Block();
            Player.ledgeStartPosition = new Vector2(Player.cornerPosition.x + Player.wallDirection * (Player.StartLedgeOffset.x + 0.02f),
                                                    Player.cornerPosition.y - Player.StartLedgeOffset.y - 0.02f);
            Player.ledgeEndPosition = new Vector2(Player.cornerPosition.x - Player.wallDirection * Player.EndLedgeOffset.x,
                                                  Player.cornerPosition.y + Player.EndLedgeOffset.y + 0.02f);
            Player.HoldPosition(Player.ledgeStartPosition);
        });

        updateActions.Add(() =>
        {
            Player.HoldPosition(Player.ledgeStartPosition);
        });

        exitActions.Add(() =>
        {
            _dashAbility.Unlock();
        });

        animationFinishActions.Add(() =>
        {
            _isHanging = true;
            _wallJumpAbility.Unlock();
            _wallJumpAbility.RestoreAmountOfUsages();
        });
    }
}
