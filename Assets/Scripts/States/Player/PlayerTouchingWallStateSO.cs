using UnityEditor;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTouchingWallState", menuName = "States/Touching Wall/Player")]

public class PlayerTouchingWallStateSO : StateSO
{
	[HideInInspector] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();

		transitions.Add(new TransitionItem(entity.states.inAir, InAirCondition, InAirAction));
		transitions.Add(new TransitionItem(entity.states.grounded, GroundedCondition, GroundedAction));
		transitions.Add(new TransitionItem(entity.states.onLedge, OnLedgeCondition, OnLedgeAction));

		enterActions.Add(() =>
		{
			entity.HoldDirection(-entity.checkers.wallDirection);
			entity.MoveToX(entity.checkers.wallPosition.x + entity.checkers.wallDirection * (entity.Size.x / 2 + 0.02f));
			entity.abilities.wallJump.RestoreAmountOfUsages();
			entity.abilities.jump.SetAmountOfUsagesToZero();
			entity.abilities.attack.HoldDirection(entity.checkers.wallDirection);
		});

		exitActions.Add(() =>
		{
			entity.ReleaseDirection();
			entity.RotateIntoDirection(entity.realDirection);
			entity.abilities.attack.ReleaseDirection();
		});
	}

	protected virtual bool InAirCondition()
	{
		return !entity.checkers.touchingWall ||
					 (entity.controller.move.x != entity.facingDirection && !entity.controller.grab);
	}

	protected virtual bool GroundedCondition()
	{
		return !entity.checkers.touchingWall ||
					 (entity.controller.move.x != entity.facingDirection && !entity.controller.grab);
	}

	protected virtual bool OnLedgeCondition()
	{
		return entity.checkers.touchingWall &&
																 !entity.checkers.touchingLedge &&
																 !entity.checkers.groundClose;
	}

	protected virtual void InAirAction()
	{
		if (!entity.abilities.wallJump.isActive)
		{
			entity.abilities.wallJump.StartCoyoteTime();
		}
	}

	protected virtual void GroundedAction()
	{
		
	}

	protected virtual void OnLedgeAction()
	{
		entity.checkers.DetermineLedgePosition();
	}
}
