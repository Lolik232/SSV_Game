using System;
using All.Interfaces;

namespace Spells
{
    public abstract class EffectApplyStrategy
    {
        public abstract void OnApply();
        public abstract bool CanApply();

        // public abstract void LogicUpdate();
    }
}