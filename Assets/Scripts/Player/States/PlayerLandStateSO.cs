using System.Collections;
using System.Collections.Generic;

using All.Events;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLandState", menuName = "State Machine/States/Player/Sub States/Land")]

public class PlayerLandStateSO : PlayerSubStateSO
{
    [SerializeField] private PlayerMoveStateSO _toMoveState;
    [SerializeField] private PlayerIdleStateSO _toIdleState;

    private bool _isLandFinished;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Player.SetVelocityX(0f);
        _isLandFinished = false;
    }

    public override void OnAnimationFinishTrigger()
    {
        base.OnAnimationFinishTrigger();
        _isLandFinished = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        transitions.Add(new TransitionItem(_toIdleState, () => _isLandFinished));
        transitions.Add(new TransitionItem(_toMoveState, () => Player.MoveInput.x != 0));
    }
}
