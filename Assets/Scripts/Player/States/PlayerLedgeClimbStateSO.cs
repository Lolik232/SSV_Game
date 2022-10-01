using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeClimbState", menuName = "State Machine/States/Player/Sub States/Ledge Climb")]

public class PlayerLedgeClimbStateSO : PlayerOnLedgeStateSO
{
    private bool _isAnimationFinished;

    [SerializeField] private PlayerIdleStateSO _toIdleState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => _isAnimationFinished));

        enterActions.Add(() =>
        {
            _isAnimationFinished = false;    
        });

        exitActions.Add(() =>
        {
            Player.transform.position = Player.ledgeEndPosition;
        });

        animationFinishActions.Add(() =>
        {
            _isAnimationFinished = true;
        });
    }
}
