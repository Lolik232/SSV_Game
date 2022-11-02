using UnityEngine;

public abstract class MoveForwardAbilitySO : Ability
{
	//public Parameter accelerationTime;

	//private float _startSpeed;
	//private float _endSpeed;
	//private float _cureentSpeed;

	//protected Movable movable;

	//protected override void OnEnable()
	//{
	//	base.OnEnable();

	//	enterConditions.Add(() => entity.controller.move.x == movable.FacingDirection);

	//	exitConditions.Add(() => entity.controller.move.x != movable.FacingDirection);

	//	enterActions.Add(() =>
	//	{
	//		_startSpeed = Mathf.Abs(movable.Velocity.x);
	//		_endSpeed = movable.MoveForwardSpeed;
	//	});

	//	updateActions.Add(() =>
	//	{
	//		Accelerate();
	//		movable.TrySetVelocityX(movable.FacingDirection * _cureentSpeed);
	//		movable.TryRotateIntoDirection(entity.controller.move.x);
	//	});

	//	exitActions.Add(() =>
	//	{
	//		movable.TrySetVelocityX(0f);
	//	});
	//}

	//public override void Initialize(GameObject origin)
	//{
	//	base.Initialize(origin);
	//	movable = origin.GetComponent<Movable>();
	//	accelerationTime.Set(accelerationTime.Max);
	//}

	//private void Accelerate()
	//{
	//	if (ActiveTime > accelerationTime)
	//	{
	//		_cureentSpeed = _endSpeed;
	//	}
	//	else
	//	{
	//		_cureentSpeed = Mathf.Lerp(_startSpeed, _endSpeed, ActiveTime / accelerationTime);
	//	}
	//}
}
