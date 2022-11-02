using UnityEngine;

public abstract class MoveBackwardAbilitySO : Ability
{
	//protected Movable movable;

	//protected override void OnEnable()
	//{
	//	base.OnEnable();

	//	enterConditions.Add(() => entity.controller.move.x == -movable.FacingDirection);

	//	exitConditions.Add(() => entity.controller.move.x != -movable.FacingDirection);

	//	updateActions.Add(() =>
	//	{
	//		movable.TrySetVelocityX(-movable.FacingDirection * movable.MoveBackwardSpeed);
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
	//	movable = origin.AddComponent<Movable>();
	//}
}
