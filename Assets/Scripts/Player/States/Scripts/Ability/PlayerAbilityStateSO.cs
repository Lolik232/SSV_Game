using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAbilityStateSO : PlayerStateSO
{
    protected bool isGroundedTransitionBlock;
    protected bool abilityDone;

    [Header("Super State Transitions")]
    [SerializeField] private PlayerIdleStateSO _toIdleState;
    [SerializeField] private PlayerInAirStateSO _toInAirState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toIdleState, () => abilityDone && !isGroundedTransitionBlock && Player.isGrounded));
        transitions.Add(new TransitionItem(_toInAirState, () => abilityDone && (!Player.isGrounded || isGroundedTransitionBlock)));

        enterActions.Add(() =>
        {
            isGroundedTransitionBlock = false;
            abilityDone = false;
        });
    }
}
