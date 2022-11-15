using UnityEngine;

namespace Systems.SpellSystem.SpellEffect.SpellLiveCycle
{
    public abstract class LiveCycleSO : ScriptableObject
    {
        public abstract BaseLiveCycle CreateLiveCycle();
    }
}