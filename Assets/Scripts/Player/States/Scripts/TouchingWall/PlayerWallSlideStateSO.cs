using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallSlideState", menuName = "Player/States/Touching Wall/Wall Slide")]

public class PlayerWallSlideStateSO : PlayerTouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallGrabCondition() => inputReader.moveInput.y >= 0 &&
																inputReader.grabInput;

		transitions.Add(new TransitionItem(states.wallGrab, WallGrabCondition));

		enterActions.Add(() =>
		{
			player.transform.Rotate(0f, 180f, 0f);
			abilities.attack.HoldDirection(player.wallDirection);
		});

		updateActions.Add(() =>
		{
			player.TrySetVelocityY(-parameters.wallSlideSpeed);
		});

		exitActions.Add(() =>
		{
			abilities.attack.ReleaseDirection();
			player.transform.Rotate(0f, 180f, 0f);
		});
	}
}
