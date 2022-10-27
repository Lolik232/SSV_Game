using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallClimbState", menuName = "Player/States/Touching Wall/Wall Climb")]

public class PlayerWallClimbStateSO : TouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallGrabCondition() => entity.controller.move.y <= 0 || 
																 !entity.controller.grab;

		transitions.Add(new TransitionItem(entity.states.wallGrab, WallGrabCondition));

		updateActions.Add(() =>
		{
			entity.TrySetVelocityY(entity.parameters.wallClimbSpeed);
		});
	}
}
