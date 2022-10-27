using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilitiesManager", menuName = "Managers/Abilities/Player")]

public class PlayerAbilitiesManagerSO : AbilitiesManagerSO
{
	[HideInInspector] public new PlayerMoveUpAbilitySO moveUp;
	[HideInInspector] public new  PlayerMoveDownAbilitySO moveDown;
	[HideInInspector] public new  PlayerMoveForwardAbilitySO moveForward;
	[HideInInspector] public new  PlayerMoveBackwardAbilitySO moveBackward;

	public PlayerAttackAbilitySO attack;
	public PlayerDashAbilitySO dash;
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
		elements.Add(jump);
		elements.Add(wallJump);
	}
}