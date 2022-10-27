public abstract class StateMachineSO : SingleTaskManagerSO<StateSO>
{
	public GroundedStateSO grounded;
	public TouchingWallStateSO touchingWall;
	public InAirStateSO inAir;

	protected override void OnEnable()
	{
		base.OnEnable();

		elements.Add(grounded);
		elements.Add(touchingWall);
		elements.Add(inAir);
	}
}
