using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallGrabState", menuName = "State Machine/States/Player/Sub States/Wall Grab")]

public class PlayerWallGrabStateSO : PlayerTouchingWallStateSO
{
    private Vector2 _holdPosition;

    [Header("State Transitions")]
    [SerializeField] private PlayerWallSlideStateSO _toWallSlideState;
    [SerializeField] private PlayerWallClimbStateSO _toWallClimbState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallClimbState, () => Player.moveInput.y > 0));
        transitions.Add(new TransitionItem(_toWallSlideState, () => Player.moveInput.y < 0 || !Player.grabInput));

        enterActions.Add(() =>
        {
            _holdPosition = Player.transform.position;
            Player.HoldPosition(_holdPosition);
        });

        updateActions.Add(() => { Player.HoldPosition(_holdPosition); });
    }
}
