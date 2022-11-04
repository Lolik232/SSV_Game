public interface IRotateable
{
	public int FacingDirection
	{
		get;
	}
	public int BodyDirection
	{
		get;
	}

	public void RotateIntoDirection(int direction);

	public void RotateBodyIntoDirection(int direction);
}
