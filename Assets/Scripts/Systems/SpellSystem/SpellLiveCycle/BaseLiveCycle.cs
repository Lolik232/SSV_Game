using System;
using All.Interfaces;

namespace Systems.SpellSystem.SpellEffect.SpellLiveCycle
{
    [Serializable]
    public abstract class BaseLiveCycle : ILiveCycle
    {
        public abstract void LogicUpdate();
        public abstract void Start();
        public abstract void Reset();
        public abstract bool IsEnd();
    }
}