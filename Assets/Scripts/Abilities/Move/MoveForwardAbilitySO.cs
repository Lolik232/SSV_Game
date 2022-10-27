using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class MoveForwardAbilitySO : AbilitySO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		enterConditions.Add(() => entity.controller.move.x == entity.facingDirection);

		exitConditions.Add(() => entity.controller.move.x != entity.facingDirection);

		updateActions.Add(() =>
		{
			entity.TryRotateIntoDirection(entity.controller.move.x);
			entity.TrySetVelocityX(entity.facingDirection * entity.parameters.moveForwardSpeed);
		});

		exitActions.Add(() =>
		{
			entity.TrySetVelocityZero();
		});
	}
}
