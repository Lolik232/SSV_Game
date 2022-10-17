using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallGrabState", menuName = "Player/States/Touching Wall/Wall Grab")]

public class PlayerWallGrabStateSO : PlayerTouchingWallStateSO
{
	private int _gravityId;
	private int _velocityId;

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
			Tuple<int, int> ids = player.HoldPosition(player.Position);
			_gravityId = ids.Item1;
			_velocityId = ids.Item2;
		});

		exitActions.Add(() =>
		{
			player.ReleasePosition(_gravityId, _velocityId);
		});
	}
}
