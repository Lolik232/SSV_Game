using UnityEngine;

public class LedgeClimbAS : AbilityState<LedgeClimbAbility>
{
    [SerializeField] private float _duration;

    protected override void Start()
    {
        base.Start();
        SetAnimationSpeed("LedgeClimb", _duration);
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();

        if (ActiveTime > _duration)
        {
            Ability.OnExit();
        }
    }
}
