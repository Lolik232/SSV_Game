public class Blocker
{
    private int _amountOfBloks = 0;

    public bool IsLocked => _amountOfBloks > 0;

    public void AddBlock()
    {
        _amountOfBloks++;
    }

    public void RemoveBlock()
    {
        if (_amountOfBloks > 0)
        {
            _amountOfBloks--;
        }
    }
}
