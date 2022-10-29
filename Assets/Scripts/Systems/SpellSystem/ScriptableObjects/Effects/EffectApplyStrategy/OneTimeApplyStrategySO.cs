using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(fileName = "Spell/Effect/Apply Strategy/One Time", menuName = "apply strategy")]
    public class OneTimeApplyStrategySO : EffectApplyStrategySO
    {
        public override EffectApplyStrategy CreateStrategy()
        {
            return new OneTimeApplyStrategy(this);
        }
    }
}