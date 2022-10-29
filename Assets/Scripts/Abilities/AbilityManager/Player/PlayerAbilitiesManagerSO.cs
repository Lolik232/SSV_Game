using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilitiesManager", menuName = "Managers/Abilities/Player")]

public class PlayerAbilitiesManagerSO : AbilitiesManagerSO
{
	[HideInInspector] [NonSerialized] public new PlayerMoveUpAbilitySO moveUp;
	[HideInInspector] [NonSerialized] public new  PlayerMoveDownAbilitySO moveDown;
	[HideInInspector] [NonSerialized] public new  PlayerMoveForwardAbilitySO moveForward;
	[HideInInspector] [NonSerialized] public new  PlayerMoveBackwardAbilitySO moveBackward;

	public PlayerAttackAbilitySO attack;
	public PlayerDashAbilitySO dash;
	public PlayerGrabAbilitySO grab;
	public PlayerLedgeClimbAbilitySO ledgeClimb;
	public PlayerJumpAbilitySO jump;
	public PlayerWallJumpAbilitySO wallJump;

	protected override void OnEnable()
	{
		moveUp = (PlayerMoveUpAbilitySO)base.moveUp;
		moveDown = (PlayerMoveDownAbilitySO)base.moveDown;
		moveForward = (PlayerMoveForwardAbilitySO)base.moveForward;
		moveBackward = (PlayerMoveBackwardAbilitySO)base.moveBackward;

		base.OnEnable();

		elements.Add(attack);
		elements.Add(dash);
		elements.Add(grab);
		elements.Add(ledgeClimb);
		elements.Add(jump);
		elements.Add(wallJump);
	}
}