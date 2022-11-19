public class StandAS : AbilityState<CrouchAbility>
{
    protected override void Start()
    {
        base.Start();
        bool CrouchCondition() => Entity.Behaviour.Move.y == -1;

        Transitions.Add(new(Ability.Crouch, CrouchCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Entity.Stand();
        Entity.DoChecks();
    }
}
