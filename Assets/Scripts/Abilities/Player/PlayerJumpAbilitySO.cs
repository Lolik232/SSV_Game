using UnityEngine;

[CreateAssetMenu(fileName = "PayerJumpAbility", menuName = "Player/Abilities/Jump")]

public class PlayerJumpAbilitySO : Ability
{
	//[SerializeField] private float _coyoteTime;

	//private float _startCoyoteTime;

	//protected Movable movable;

	//protected override void OnEnable()
	//{
	//	base.OnEnable();

	//	enterConditions.Add(() =>
	//	{
	//		return entity.controller.jump &&
	//					 !entity.checkers.touchingCeiling;
	//	});

	//	exitConditions.Add(() =>
	//	{
	//		return movable.Velocity.y < 0f ||
	//					 !entity.controller.jumpInputHold;
	//	});

	//	enterActions.Add(() =>
	//	{
	//		entity.controller.jump = false;
	//		movable.SetVelocityY(entity.parameters.jumpForce);
	//	});

	//	exitActions.Add(() =>
	//	{
	//		if (movable.Velocity.y > 0f)
	//		{
	//			movable.SetVelocityY(movable.Velocity.y * 0.5f);
	//		}
	//	});
	//}

	//public override void Initialize(GameObject origin)
	//{
	//	base.Initialize(origin);
	//	movable = origin.GetComponent<Movable>();
	//}

	//public bool IsCoyoteTime() => Time.time < _startCoyoteTime + _coyoteTime;

	//public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
