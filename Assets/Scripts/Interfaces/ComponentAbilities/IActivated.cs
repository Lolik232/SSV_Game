public interface IActivated
{
	public bool IsActive
	{
		get;
	}

	public float ActiveTime
	{
		get;
	}

	public float InactiveTime
	{
		get;
	}

	public void OnEnter();

	public void OnExit();
}
