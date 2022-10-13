using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallSlideState", menuName = "Player/States/Touching Wall/Wall Slide")]

public class PlayerWallSlideStateSO : PlayerTouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallGrabCondition() => Player.moveInput.y >= 0 && 
																Player.grabInput;

		transitions.Add(new TransitionItem(states.wallGrab, WallGrabCondition));

		enterActions.Add(() =>
		{
			Player.transform.Rotate(0f, 180f, 0f);
		});

		updateActions.Add(() => { Player.TrySetVelocityY(-Player.WallSlideSpeed); });

		exitActions.Add(() =>
		{
			Player.transform.Rotate(0f, 180f, 0f);
		});
	}
}
