public interface IBlockable
{
	public void Block(bool needHardExit);

	public void Unlock();
}