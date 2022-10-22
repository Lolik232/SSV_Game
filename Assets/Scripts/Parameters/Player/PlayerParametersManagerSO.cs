using System;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParametersManager", menuName = "Player/Parameters/Parameters Manager")]

public class PlayerParametersManagerSO : ScriptableObject
{
	[NonSerialized] public List<Parameter> parameters;

	public Parameter moveSpeed;
	public Parameter crouchMoveSpeed;
	public Parameter inAirMoveSpeed;
	public Parameter wallSlideSpeed;
	public Parameter wallClimbSpeed;

	public Parameter dashForce;

	public Parameter jumpForce;
	public Parameter wallJumpForce;

	public Parameter swordAttackDistance;
	public Parameter swordAttackAngle;

	private void OnEnable()
	{
		parameters = new List<Parameter>
		{
				moveSpeed,
				crouchMoveSpeed,
				inAirMoveSpeed,
				wallClimbSpeed,
				wallSlideSpeed,

				dashForce,
				jumpForce,
				wallJumpForce,

				swordAttackDistance,
				swordAttackAngle
		};
	}

	public void Initialize()
	{
		foreach (var parameter in parameters)
		{
			parameter.Set(parameter.Max);
		}
	}
}
