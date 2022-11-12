using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    public abstract class EffectApplyStrategySO : ScriptableObject
    {
        public abstract EffectApplyStrategy CreateStrategy();
    }
}