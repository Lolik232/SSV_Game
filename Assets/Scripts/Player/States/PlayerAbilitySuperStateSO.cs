using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitySuperStateSO : PlayerStateSO
{
    private bool _abilityDone;
    public bool AbilityDone => _abilityDone;

    private bool _isGrounded;

    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerInAirStateSO _toInAirState;

    protected override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = Player.CheckIfGrounded();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => _abilityDone && _isGrounded ));
        transitions.Add(new TransitionItem(_toInAirState, () => _abilityDone && !_isGrounded));
    }
}
