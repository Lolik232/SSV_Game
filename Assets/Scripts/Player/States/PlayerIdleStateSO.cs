using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIdleState", menuName = "State Machine/States/Player/Sub States/Idle")]
public class PlayerIdleStateSO : PlayerSubStateSO
{
    [SerializeField] private PlayerMoveStateSO _toMoveState;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Player.SetVelocityX(0f);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        transitions.Add(new TransitionItem(_toMoveState, () => Player.MoveInput.x != 0));
    }
}
