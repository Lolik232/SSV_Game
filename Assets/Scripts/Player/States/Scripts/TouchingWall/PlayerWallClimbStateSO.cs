using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallClimbState", menuName = "Player/States/Touching Wall/Wall Climb")]

public class PlayerWallClimbStateSO : PlayerTouchingWallStateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		bool WallClimbCondition() => inputReader.moveInput.y < 1;

		transitions.Add(new TransitionItem(states.wallGrab, WallClimbCondition));

		updateActions.Add(() =>
		{
			player.TrySetVelocityY(player.WallClimbSpeed);
		});
	}
}
