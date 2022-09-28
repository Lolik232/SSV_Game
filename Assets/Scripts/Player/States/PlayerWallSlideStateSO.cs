using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallSlideState", menuName = "State Machine/States/Player/Sub States/Wall Slide")]

public class PlayerWallSlideStateSO : PlayerSubStateSO
{
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.moveInput.y >= 0f && Player.grabInput));

        updateActions.Add(() => { Player.SetVelocityY(-Player.WallSlideSpeed); });
    }
}