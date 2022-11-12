
using UnityEngine;

public class PlayerWallGrabAS : StayAS<PlayerMoveVerticalAbility>
{
	private void Start()
	{
		bool ClimbCondition() => Ability.Player.Input.Grab && Ability.Player.Input.Move.y == 1 &&
														 Ability.Player.Velocity.y > -0.01f;

		bool SlideCondition() => (Ability.Player.Input.Grab && Ability.Player.Input.Move.y == -1 || !Ability.Player.Input.Grab) &&
														 Ability.Player.Velocity.y < 0.01f;

		Transitions.Add(new(Ability.Climb, ClimbCondition));
		Transitions.Add(new(Ability.Slide, SlideCondition));
	}

	protected override void ApplyEnterActions()
	{
		StartSpeed = Ability.Player.Velocity.y;
		base.ApplyEnterActions();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		Ability.Player.SetVelocityY(MoveSpeed);
	}
}
