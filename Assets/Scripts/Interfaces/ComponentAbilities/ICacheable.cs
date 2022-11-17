public interface ICacheable<T>
{
    public T Value
    {
        get;
    }

    public bool IsLocked
    {
        get;
    }

    public bool TrySet(T value);

    public int Hold(T value);

    public void Release(int id);
}
