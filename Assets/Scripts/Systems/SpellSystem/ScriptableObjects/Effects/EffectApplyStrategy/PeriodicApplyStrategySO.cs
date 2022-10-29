using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(fileName = "Spell/Effect/Apply Strategy/Periodic Time", menuName = "apply strategy")]
    public class PeriodicApplyStrategySO : EffectApplyStrategySO
    {
        public override EffectApplyStrategy CreateStrategy()
        {
            return new PeriodicApplyStrategy(this);
        }
    }
}