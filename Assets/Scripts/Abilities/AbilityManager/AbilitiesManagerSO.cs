public abstract class AbilitiesManagerSO : MultiTaskManagerSO<AbilitySO>
{
	public MoveUpAbilitySO moveUp;
	public MoveDownAbilitySO moveDown;
	public MoveForwardAbilitySO moveForward;
	public MoveBackwardAbilitySO moveBackward;

	protected override void OnEnable()
	{
		base.OnEnable();

		elements.Add(moveUp);
		elements.Add(moveDown);
		elements.Add(moveForward);
		elements.Add(moveBackward);
	}
}
