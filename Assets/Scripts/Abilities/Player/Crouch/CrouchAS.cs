public class CrouchAS : AbilityState<CrouchAbility>
{

    protected override void Start()
    {
        base.Start();
        bool StandCondition() => !Entity.TouchingCeiling && Entity.Behaviour.Move.y != -1;

        Transitions.Add(new(Ability.Stand, StandCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Entity.Crouch();
        Entity.DoChecks();
        
        if (_clip != null && Entity.Grounded)
        {
            Entity.Source.PlayOneShot(_clip);
        }
    }

    protected void OnCrouchStep()
    {
        if (_clip != null)
        {
            Entity.Source.PlayOneShot(_clip);
        }
    }
}
