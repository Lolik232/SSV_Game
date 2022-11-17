public interface IParameter<T>
{
    public T Min
    {
        get;
    }
    public T Max
    {
        get;
    }
    public T Addition
    {
        get;
        set;
    }
    public T Current
    {
        get;
        set;
    }

    public void Initialize();

    public void Set(T value);
}
