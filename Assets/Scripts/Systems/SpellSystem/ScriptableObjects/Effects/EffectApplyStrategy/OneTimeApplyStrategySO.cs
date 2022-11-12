using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [CreateAssetMenu(menuName = "Spell/Effect/Apply Strategy/One Time")]
    public class OneTimeApplyStrategySO : EffectApplyStrategySO
    {
        public override EffectApplyStrategy CreateStrategy()
        {
            return new OneTimeApplyStrategy(this);
        }
    }
}