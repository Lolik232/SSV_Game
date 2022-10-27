using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParametersManager", menuName = "Managers/Parameters/Player")]

public class PlayerParametersManagerSO : ParametersManagerSO
{
	public Parameter dashForce;

	public Parameter jumpForce;
	public Parameter wallJumpForce;

	protected override void OnEnable()
	{
		base.OnEnable();

		elements.Add(dashForce);
		elements.Add(jumpForce);
		elements.Add(wallJumpForce);
	}
}
