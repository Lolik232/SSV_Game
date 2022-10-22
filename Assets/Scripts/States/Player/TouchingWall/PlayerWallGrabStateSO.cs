using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallGrabState", menuName = "Player/States/Touching Wall/Wall Grab")]

public class PlayerWallGrabStateSO : PlayerTouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallClimbCondition() => data.input.moveInput.y > 0;

		bool WallSlideCondition() => data.input.moveInput.y < 0 ||
																 !data.input.grabInput;

		transitions.Add(new TransitionItem(data.states.wallSlide, WallSlideCondition));
		transitions.Add(new TransitionItem(data.states.wallClimb, WallClimbCondition));

		enterActions.Add(() =>
		{
			entity.HoldPosition(entity.Position);
		});

		exitActions.Add(() =>
		{
			entity.ReleasePosition();
		});
	}
}
