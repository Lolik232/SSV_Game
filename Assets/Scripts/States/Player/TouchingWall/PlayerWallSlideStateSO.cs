using System.Runtime.CompilerServices;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallSlideState", menuName = "Player/States/Touching Wall/Wall Slide")]

public class PlayerWallSlideStateSO : PlayerTouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallGrabCondition() => data.input.moveInput.y >= 0 &&
																data.input.grabInput;

		transitions.Add(new TransitionItem(data.states.wallGrab, WallGrabCondition));

		enterActions.Add(() =>
		{
			needHadFlip = true;
			entity.SoftFlip();
		});

		updateActions.Add(() =>
		{
			entity.TrySetVelocityY(-data.parameters.wallSlideSpeed);
		});

		exitActions.Add(() =>
		{
			entity.SoftFlip();
		});
	}
}
