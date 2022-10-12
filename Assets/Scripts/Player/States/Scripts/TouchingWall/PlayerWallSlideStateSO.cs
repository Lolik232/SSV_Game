using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "PlayerWallSlideState", menuName = "Player/States/Touching Wall/Wall Slide")]

public class PlayerWallSlideStateSO : PlayerTouchingWallStateSO
{
    [Header("State Transitions")]
    [SerializeField] private PlayerWallGrabStateSO _toWallGrabState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallGrabState, () => Player.moveInput.y >= 0 && Player.grabInput));

        enterActions.Add(() =>
        {
            Player.transform.Rotate(0f, 180f, 0f);
        });

        updateActions.Add(() => { Player.TrySetVelocityY(-Player.WallSlideSpeed); });

        exitActions.Add(() =>
        {
            Player.transform.Rotate(0f, 180f, 0f);
        });
    }
}
