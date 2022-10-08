using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveState", menuName = "State Machine/States/Player/Sub States/Move")]
public class PlayerMoveStateSO : PlayerGroundedStateSO
{
    [Header("State Transitions")]
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerCrouchMoveStateSO _toCrouchMoveState;
    [SerializeField] private PlayerLedgeClimbStateSO _toLedgeClimbState;


    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => Player.moveInput.x == 0));
        transitions.Add(new TransitionItem(_toCrouchMoveState, () => Player.moveInput.y < 0));
        transitions.Add(new TransitionItem(_toLedgeClimbState, () => Player.isTouchingWall && !Player.isTouchingLedge && Player.moveInput.x == -Player.wallDirection, () =>
        {
            Player.ledgeStartPosition = new Vector2(Player.cornerPosition.x + Player.wallDirection * (Player.StartLedgeOffset.x + 0.02f),
                                                    Player.cornerPosition.y - Player.StartLedgeOffset.y - 0.02f);
            Player.ledgeEndPosition = new Vector2(Player.cornerPosition.x - Player.wallDirection * Player.EndLedgeOffset.x,
                                                  Player.cornerPosition.y + Player.EndLedgeOffset.y + 0.02f);
        }));

        updateActions.Add(() =>
        {
            Player.CheckIfShouldFlip(Player.moveInput.x);
            Player.SetVelocityX(Player.moveInput.x * Player.MoveSpeed);
        });

        checks.Add(() =>
        {
            Player.CheckIfTouchingWallBack();
            if (Player.isTouchingWall && !Player.isTouchingLedge)
            {
                Player.DeterminCornerPosition();
            }
        });
    }
}
