using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallSlideState", menuName = "Player/States/Touching Wall/Wall Slide")]

public class PlayerWallSlideStateSO : PlayerTouchingWallStateSO
{
    [Header("State Transitions")]
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.moveInput.y >= 0 && Player.grabInput));

        updateActions.Add(() => { Player.SetVelocityY(-Player.WallSlideSpeed); });
    }
}
