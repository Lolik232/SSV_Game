using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class MoveDownAbilitySO : AbilitySO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		enterConditions.Add(() => entity.controller.move.y == -1);

		exitConditions.Add(() => entity.controller.move.y != -1);

		updateActions.Add(() =>
		{
			entity.TrySetVelocityY(-entity.parameters.moveDownSpeed);
		});

		exitActions.Add(() =>
		{
			entity.TrySetVelocityZero();
		});
	}
}

