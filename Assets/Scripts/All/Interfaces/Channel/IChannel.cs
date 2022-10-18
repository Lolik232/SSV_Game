namespace All.Interfaces.Channel
{
    public interface IChannel : IReadChannel, IWriteChannel
    {
    }


    public interface IChannel<T1, T2> : IReadChannel<T1, T2>, IWriteChannel<T1, T2>
    {
    }

    public interface IChannel<T1, T2, T3> : IReadChannel<T1, T2, T3>, IWriteChannel<T1, T2, T3>
    {
    }
}