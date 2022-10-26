using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallClimbState", menuName = "Player/States/Touching Wall/Wall Climb")]

public class PlayerWallClimbStateSO : PlayerTouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallGrabCondition() => data.controller.move.y <= 0 || 
																 !data.controller.grab;

		transitions.Add(new TransitionItem(data.states.wallGrab, WallGrabCondition));

		updateActions.Add(() =>
		{
			entity.TrySetVelocityY(data.parameters.wallClimbSpeed);
		});
	}
}
