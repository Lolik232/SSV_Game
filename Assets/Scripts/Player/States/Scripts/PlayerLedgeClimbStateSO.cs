using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLedgeClimbState", menuName = "Player/States/Ledge Climb")]

public class PlayerLedgeClimbStateSO : PlayerStateSO
{
	private bool _climbFinished;

	protected override void OnEnable()
	{
		base.OnEnable();

		bool IdleCondition() => _climbFinished && 
													  !Player.isTouchingCeilingWhenClimb;

		bool CrouchIdleCondition() => _climbFinished && 
																	Player.isTouchingCeilingWhenClimb;

		transitions.Add(new TransitionItem(states.idle, IdleCondition));
		transitions.Add(new TransitionItem(states.crouchIdle, CrouchIdleCondition));

		enterActions.Add(() =>
		{
			_climbFinished = false;
			Player.isClimbingLedge = true;
			Player.HoldPosition(Player.ledgeStartPosition);
		});

		updateActions.Add(() =>
		{
			Player.HoldPosition(Player.ledgeStartPosition);
		});

		exitActions.Add(() =>
		{
			Player.isClimbingLedge = false;
			Player.transform.position = Player.ledgeEndPosition;
		});

		animationFinishActions.Add(() =>
		{
			_climbFinished = true;
		});
	}
}
