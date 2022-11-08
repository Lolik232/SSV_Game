using UnityEngine;

public class PlayerMoveBackwardAS : MoveAS<PlayerMoveHorizontalAbility>
{
	private void Start()
	{
		bool StayCondition() => Ability.Player.Input.Move.x != -Ability.Player.FacingDirection;

		Transitions.Add(new(Ability.Stay, StayCondition));
	}

	protected override void ApplyEnterActions()
	{
		StartSpeed = Ability.Player.Velocity.x;
		MoveDirection = -Ability.Player.FacingDirection;
		base.ApplyEnterActions();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		Ability.Player.SetVelocityX(MoveSpeed);
		Ability.Player.RotateIntoDirection(Ability.Player.Input.Move.x);
	}
}
