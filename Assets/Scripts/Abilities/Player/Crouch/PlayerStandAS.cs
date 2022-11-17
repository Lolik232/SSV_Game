public class PlayerStandAS : AbilityState<PlayerCrouchAbility>
{
    private void Start()
    {
        bool CrouchCondition() => Ability.Player.Input.Move.y == -1;

        Transitions.Add(new(Ability.Crouch, CrouchCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Ability.Player.Stand();
        Ability.Player.DoChecks();
    }
}
