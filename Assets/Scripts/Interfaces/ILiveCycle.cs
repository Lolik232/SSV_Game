namespace All.Interfaces
{
    public interface ILiveCycle : ILogicUpdate
    {
        public void Start();
        public void Reset();
        public bool IsEnd();
    }
}