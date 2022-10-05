namespace All.Interfaces.Channel
{
    public interface IWriteChannel
    {
        public void RaiseEvent();
    }

    public interface IWriteChannel<in T>
    {
        public void RaiseEvent(T arg1);
    }

    public interface IWriteChannel<in T1, in T2>
    {
        public void RaiseEvent(T1 arg1, T2 arg2);
    }

    public interface IWriteChannel<in T1, in T2, in T3>
    {
        public void RaiseEvent(T1 arg1, T2 arg2, T3 arg3);
    }
}