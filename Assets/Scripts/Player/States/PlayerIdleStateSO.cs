using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIdleState", menuName = "State Machine/States/Player/Sub States/Idle")]
public class PlayerIdleStateSO : PlayerSubStateSO
{
    [SerializeField] private PlayerMoveStateSO _toMoveState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toMoveState, () => Player.moveInput.x != 0));

        enterActions.Add(() => { Player.SetVelocityX(0f); });
    }
}
