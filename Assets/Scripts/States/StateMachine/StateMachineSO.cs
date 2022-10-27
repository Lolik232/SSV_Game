public abstract class StateMachineSO : SingleTaskManagerSO<StateSO>
{
	public GroundedStateSO grounded;
	public InAirStateSO inAir;

	protected override void OnEnable()
	{
		base.OnEnable();

		elements.Add(grounded);
		elements.Add(inAir);
	}
}
