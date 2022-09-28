using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveState", menuName = "State Machine/States/Player/Sub States/Move")]
public class PlayerMoveStateSO : PlayerSubStateSO
{
    [SerializeField] private PlayerIdleStateSO _toIdleState;

    protected override void OnEnable()
    {
        base.OnEnable();
        transitions.Add(new TransitionItem(_toIdleState, () => Player.moveInput.x == 0));
        updateActions.Add(() =>
        {
            Player.CheckIfShouldFlip(Player.moveInput.x);
            Player.SetVelocityX(Player.moveInput.x * Player.MoveSpeed);
        });
    }
}
