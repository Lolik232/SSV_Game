using UnityEngine;


public class PlayerCrouchAS : AbilityState<PlayerCrouchAbility>
{

    private void Start()
    {
        bool StandCondition() => !Ability.Player.TouchingCeiling && Ability.Player.Input.Move.y != -1;

        Transitions.Add(new(Ability.Stand, StandCondition));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Ability.Player.Crouch();
        Ability.Player.DoChecks();
    }
}
