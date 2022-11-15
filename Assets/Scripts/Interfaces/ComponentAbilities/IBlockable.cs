public interface IBlockable
{
    public bool IsLocked
    {
        get;
    }

    public void Block();

    public void Unlock();
}