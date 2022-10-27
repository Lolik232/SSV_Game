using System.Runtime.CompilerServices;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallSlideState", menuName = "Player/States/Touching Wall/Wall Slide")]

public class PlayerWallSlideStateSO : TouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallGrabCondition() => entity.controller.move.y >= 0 &&
																entity.controller.grab;

		transitions.Add(new TransitionItem(entity.states.wallGrab, WallGrabCondition));

		enterActions.Add(() =>
		{
			entity.RotateBodyIntoDirection(entity.checkers.wallDirection);
		});

		updateActions.Add(() =>
		{
			entity.TrySetVelocityY(-entity.parameters.wallSlideSpeed);
		});
	}
}