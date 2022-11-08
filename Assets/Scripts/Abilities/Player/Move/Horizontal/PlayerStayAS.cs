using UnityEngine;

public class PlayerStayAS : StayAS<PlayerMoveHorizontalAbility>
{
	private void Start()
	{
		bool ForwardCondition() => Ability.Player.Input.Move.x == Ability.Player.FacingDirection && 
														   (Ability.Player.Velocity.x == 0 || Mathf.Sign(Ability.Player.Input.Move.x) == Mathf.Sign(Ability.Player.Velocity.x));

		bool BackwardCondition() => Ability.Player.Input.Move.x == -Ability.Player.FacingDirection && 
																(Ability.Player.Velocity.x == 0 || Mathf.Sign(Ability.Player.Input.Move.x) == Mathf.Sign(Ability.Player.Velocity.x));

		Transitions.Add(new(Ability.Forward, ForwardCondition));
		Transitions.Add(new(Ability.Backward, BackwardCondition));
	}

	protected override void ApplyEnterActions()
	{
		StartSpeed = Ability.Player.Velocity.x;
		base.ApplyEnterActions();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		Ability.Player.SetVelocityX(MoveSpeed);
	}
}
