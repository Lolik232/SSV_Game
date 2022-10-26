using System.Runtime.CompilerServices;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallSlideState", menuName = "Player/States/Touching Wall/Wall Slide")]

public class PlayerWallSlideStateSO : PlayerTouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallGrabCondition() => data.controller.move.y >= 0 &&
																data.controller.grab;

		transitions.Add(new TransitionItem(data.states.wallGrab, WallGrabCondition));

		enterActions.Add(() =>
		{
			entity.RotateBodyIntoDirection(data.checkers.wallDirection);
		});

		updateActions.Add(() =>
		{
			entity.TrySetVelocityY(-data.parameters.wallSlideSpeed);
		});
	}
}