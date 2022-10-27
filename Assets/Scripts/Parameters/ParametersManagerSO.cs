public abstract class ParametersManagerSO : StaticManagerSO<Parameter>
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

	public override void InitializeParameters()
	{
		base.InitializeParameters();
		for (int i = 0; i < elements.Count; i++)
		{
			elements[i].Set(elements[i].Max);
		}
	}
}
