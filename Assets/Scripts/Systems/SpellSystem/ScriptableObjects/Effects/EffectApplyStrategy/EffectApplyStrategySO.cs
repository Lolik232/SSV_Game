using UnityEngine;

namespace Spells
{
    public abstract class EffectApplyStrategySO : ScriptableObject
    {
        public abstract EffectApplyStrategy CreateStrategy();
    }
}