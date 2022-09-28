using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAbilityStateSO : PlayerStateSO
{
    private bool _isGroundedTransitionBlock;
    private bool _abilityDone;
    private bool _isGrounded;

    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerInAirStateSO _toInAirState;

    protected void OnAbillityDone() => _abilityDone = true;
    protected void BlockIsGroundedTransition() => _isGroundedTransitionBlock = true;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => _abilityDone && !_isGroundedTransitionBlock && _isGrounded));
        transitions.Add(new TransitionItem(_toInAirState, () => _abilityDone && !_isGrounded));
        enterActions.Add(() =>
        {
            _isGroundedTransitionBlock = false;
            _abilityDone = false;
        });
        checks.Add(() => { _isGrounded = Player.CheckIfGrounded(); });
    }
}
