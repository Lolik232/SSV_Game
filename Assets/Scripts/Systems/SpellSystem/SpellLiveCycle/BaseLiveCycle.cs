using All.Interfaces;

namespace Systems.SpellSystem.SpellLiveCycle
{
    public abstract class BaseLiveCycle : ILiveCycle
    {
        public abstract void LogicUpdate();
        public abstract void Start();
        public abstract void Reset();
        public abstract bool IsEnd();
    }
}