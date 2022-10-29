using System;

using UnityEngine;

public abstract class EntitySO : ComponentSO /*: ContainerSO, IPhysical, IMovable, ICrouchable*/
{
	public AbilitiesManagerSO abilities;
	public StateMachineSO states;
	public CheckersManagerSO checkers;
	public ParametersManagerSO parameters;
	public MoveController controller;

	//[SerializeField] private Physical _physical;
	//[SerializeField] private Movable _movable;
	//[SerializeField] private Crouchable _crouchable;

	//public Vector2 Position
	//{
	//	get => _physical.Position;
	//}
	//public Vector2 Velocity
	//{
	//	get => _physical.Velocity;
	//}
	//public float Gravity
	//{
	//	get => _physical.Gravity;
	//}
	//public int FacingDirection
	//{
	//	get => _movable.FacingDirection;
	//}
	//public int BodyDirection
	//{
	//	get => _movable.BodyDirection;
	//}
	//public Parameter MoveUpSpeed
	//{
	//	get => _movable.MoveUpSpeed;
	//	set => _movable.MoveUpSpeed = value;
	//}
	//public Parameter MoveDownSpeed
	//{
	//	get => _movable.MoveDownSpeed;
	//	set => _movable.MoveDownSpeed = value;
	//}
	//public Parameter MoveForwardSpeed
	//{
	//	get => _movable.MoveForwardSpeed;
	//	set => _movable.MoveForwardSpeed = value;
	//}
	//public Parameter MoveBackwardSpeed
	//{
	//	get => _movable.MoveBackwardSpeed;
	//	set => _movable.MoveBackwardSpeed = value;
	//}
	//public Vector2 Size
	//{
	//	get => _physical.Size;
	//}
	//public Vector2 Offset
	//{
	//	get => _physical.Offset;
	//}
	//public Vector2 Center
	//{
	//	get => _physical.Center;
	//}
	//public Vector2 StandSize
	//{
	//	get => _crouchable.StandSize;
	//}
	//public Vector2 StandOffset
	//{
	//	get => _crouchable.StandOffset;
	//}
	//public Vector2 StandCenter
	//{
	//	get => _crouchable.StandCenter;
	//}
	//public Vector2 CrouchSize
	//{
	//	get => _crouchable.CrouchSize;
	//}
	//public Vector2 CrouchOffset
	//{
	//	get => _crouchable.CrouchOffset;
	//}
	//public Vector2 CrouchCenter
	//{
	//	get => _crouchable.CrouchCenter;
	//}
	//public bool IsStanding
	//{
	//	get => _crouchable.IsStanding;
	//}

	//public void TrySetPosition(Vector2 position)
	//{
	//	_movable.TrySetPosition(position);
	//}

	//public void TrySetPositionX(float x)
	//{
	//	_movable.TrySetPositionX(x);
	//}

	//public void TrySetPositionY(float y)
	//{
	//	_movable.TrySetPositionY(y);
	//}

	//public void TrySetVelocity(Vector2 velocity)
	//{
	//	_movable.TrySetVelocity(velocity);
	//}

	//public void TrySetVelocity(float speed, Vector2 angle, int direction)
	//{
	//	_movable.TrySetVelocity(speed, angle, direction);
	//}

	//public void TrySetVelocityX(float x)
	//{
	//	_movable.TrySetVelocityX(x);
	//}

	//public void TrySetVelocityY(float y)
	//{
	//	_movable.TrySetVelocityY(y);
	//}

	//public void TrySetGravity(float gravity)
	//{
	//	_movable.TrySetGravity(gravity);
	//}

	//public void TryRotateIntoDirection(int direction)
	//{
	//	_movable.TryRotateIntoDirection(direction);
	//}

	//public void RotateBodyIntoDirection(int direction)
	//{
	//	_movable.RotateBodyIntoDirection(direction);
	//}

	//public int HoldPosition(Vector2 position)
	//{
	//	return _movable.HoldPosition(position);
	//}

	//public int HoldVeclocity(Vector2 velocity)
	//{
	//	return _movable.HoldVeclocity(velocity);
	//}

	//public int HoldGravity(float gravity)
	//{
	//	return _movable.HoldGravity(gravity);
	//}

	//public int HoldDirection(int direction)
	//{
	//	return _movable.HoldDirection(direction);
	//}

	//public void ReleasePosition(int id)
	//{
	//	_movable.ReleasePosition(id);
	//}

	//public void ReleaseVelocity(int id)
	//{
	//	_movable.ReleaseVelocity(id);
	//}

	//public void ReleaseGravity(int id)
	//{
	//	_movable.ReleaseGravity(id);
	//}

	//public void ReleaseDirection(int id)
	//{
	//	_movable.ReleaseDirection(id);
	//}

	//public void Stand()
	//{
	//	_crouchable.Stand();
	//}

	//public void Crouch()
	//{
	//	_crouchable.Crouch();
	//}

	//protected override void OnEnable()
	//{
	//	base.OnEnable();
	//}
}
