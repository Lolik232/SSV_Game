using Systems.SpellSystem.SpellLiveCycle;
using UnityEngine;

namespace Spells.SpellLiveCycle
{
    public abstract class LiveCycleSO : ScriptableObject
    {
        public abstract BaseLiveCycle CreateLiveCycle();
    }
}