using UnityEngine;

public abstract class MoveForwardAbilitySO : AbilitySO
{
	public Parameter accelerationTime;

	private float _startSpeed;
	private float _endSpeed;
	private float _cureentSpeed;

	protected override void OnEnable()
	{
		base.OnEnable();

		enterConditions.Add(() => entity.controller.move.x == entity.direction.facing);

		exitConditions.Add(() => entity.controller.move.x != entity.direction.facing);

		enterActions.Add(() =>
		{
			_startSpeed = Mathf.Abs(entity.Velocity.x);
			_endSpeed = entity.parameters.moveForwardSpeed;
		});

		updateActions.Add(() =>
		{
			Accelerate();
			entity.TrySetVelocityX(entity.direction.facing * _cureentSpeed);
			entity.TryRotateIntoDirection(entity.controller.move.x);
		});

		exitActions.Add(() =>
		{
			entity.TrySetVelocityX(0f);
		});
	}

	public override void InitializeParameters()
	{
		base.InitializeParameters();
		accelerationTime.Set(accelerationTime.Max);
	}

	private void Accelerate()
	{
		if (ActiveTime > accelerationTime)
		{
			_cureentSpeed = _endSpeed;
		}
		else
		{
			_cureentSpeed = Mathf.Lerp(_startSpeed, _endSpeed, ActiveTime / accelerationTime);
		}
	}
}
