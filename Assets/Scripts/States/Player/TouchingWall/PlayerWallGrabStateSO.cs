using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallGrabState", menuName = "Player/States/Touching Wall/Wall Grab")]

public class PlayerWallGrabStateSO : TouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallClimbCondition() => entity.controller.move.y > 0;

		bool WallSlideCondition() => entity.controller.move.y < 0 ||
																 !entity.controller.grab;

		transitions.Add(new TransitionItem(entity.states.wallSlide, WallSlideCondition));
		transitions.Add(new TransitionItem(entity.states.wallClimb, WallClimbCondition));

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
