using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveState", menuName = "State Machine/States/Player/Sub States/Move")]
public class PlayerMoveStateSO : PlayerSubStateSO
{
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    public float moveSpeed;

    public override void OnUpdate()
    {
        base.OnUpdate();
        Player.CheckIfShouldFlip(Player.MoveInput.x);
        Player.SetVelocityX(Player.MoveInput.x * Player.MoveSpeed);
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        transitions.Add(new TransitionItem(_toIdleState, () => Player.MoveInput.x == 0));
    }
}
