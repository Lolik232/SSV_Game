using UnityEngine;

[CreateAssetMenu(fileName = "PayerAttackAbility", menuName = "Player/Abilities/Attack")]

public class PlayerAttackAbilitySO : PlayerAbilitySO
{

	protected override void OnEnable()
	{
		base.OnEnable();

		useConditions.Add(() => inputReader.attackInput);

		useActions.Add(() =>
		{
			inputReader.attackInput = false;
			player.weapon.OnEnter();
		});

		terminateActions.Add(() =>
		{
			player.weapon.OnExit();
		});
	}

	public void HoldDirection(int direction)
	{
		player.weapon.HoldDirection(direction);
	}

	public void ReleaseDirection()
	{
		player.weapon.ReleaseDirection();
	}
}
