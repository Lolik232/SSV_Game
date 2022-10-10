using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallClimbState", menuName = "Player/States/Touching Wall/Wall Climb")]

public class PlayerWallClimbStateSO : PlayerTouchingWallStateSO
{
    [Header("State Transitions")]
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.moveInput.y < 1));

        updateActions.Add(() => { Player.SetVelocityY(Player.WallClimbSpeed); });
    }
}
