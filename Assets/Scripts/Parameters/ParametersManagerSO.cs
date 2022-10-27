public abstract class ParametersManagerSO : ManagerSO<Parameter>
{
	public Parameter moveUpSpeed;
	public Parameter moveDownSpeed;
	public Parameter moveForwardSpeed;
	public Parameter moveBackwardSpeed;

	protected override void OnEnable()
	{
		base.OnEnable();

		elements.Add(moveUpSpeed);
		elements.Add(moveDownSpeed);
		elements.Add(moveForwardSpeed);
		elements.Add(moveBackwardSpeed);
	}
}
