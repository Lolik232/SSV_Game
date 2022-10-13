using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallGrabState", menuName = "Player/States/Touching Wall/Wall Grab")]

public class PlayerWallGrabStateSO : PlayerTouchingWallStateSO
{
	private Vector2 _holdPosition;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallClimbCondition() => Player.moveInput.y > 0;

		bool WallSlideCondition() => Player.moveInput.y < 0 || 
																 !Player.grabInput;

		transitions.Add(new TransitionItem(states.wallClimb, WallClimbCondition));
		transitions.Add(new TransitionItem(states.wallSlide, WallSlideCondition));

		enterActions.Add(() =>
		{
			_holdPosition = Player.transform.position;
			Player.HoldPosition(_holdPosition);
		});

		updateActions.Add(() =>
		{
			Player.HoldPosition(_holdPosition);
		});
	}
}
