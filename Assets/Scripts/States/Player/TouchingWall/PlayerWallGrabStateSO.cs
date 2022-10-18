using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallGrabState", menuName = "Player/States/Touching Wall/Wall Grab")]

public class PlayerWallGrabStateSO : PlayerTouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallClimbCondition() => inputReader.moveInput.y > 0;

		bool WallSlideCondition() => inputReader.moveInput.y < 0 ||
																 !inputReader.grabInput;

		transitions.Add(new TransitionItem(states.wallClimb, WallClimbCondition));
		transitions.Add(new TransitionItem(states.wallSlide, WallSlideCondition));

		enterActions.Add(() =>
		{
			player.HoldPosition(player.Position);
		});

		exitActions.Add(() =>
		{
			player.ReleasePosition();
		});
	}
}
