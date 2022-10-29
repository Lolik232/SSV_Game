public abstract class MoveBackwardAbilitySO : AbilitySO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		enterConditions.Add(() => entity.controller.move.x == -entity.direction.facing);

		exitConditions.Add(() => entity.controller.move.x != -entity.direction.facing);

		updateActions.Add(() =>
		{
			entity.TrySetVelocityX(-entity.direction.facing * entity.parameters.moveBackwardSpeed);
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
	}
}
