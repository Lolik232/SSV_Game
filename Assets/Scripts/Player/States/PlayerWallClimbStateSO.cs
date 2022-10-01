using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallClimbState", menuName = "State Machine/States/Player/Sub States/Wall Climb")]

public class PlayerWallClimbStateSO : PlayerTouchingWallStateSO
{
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.moveInput.y < 1f));

        updateActions.Add(() => { Player.SetVelocityY(Player.WallClimbSpeed); });
    }
}
