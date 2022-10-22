using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallClimbState", menuName = "Player/States/Touching Wall/Wall Climb")]

public class PlayerWallClimbStateSO : PlayerTouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallGrabCondition() => data.input.moveInput.y <= 0 || 
																 !data.input.grabInput;

		transitions.Add(new TransitionItem(data.states.wallGrab, WallGrabCondition));

		updateActions.Add(() =>
		{
			entity.TrySetVelocityY(data.parameters.wallClimbSpeed);
		});
	}
}
