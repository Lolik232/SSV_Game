using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [CreateAssetMenu(menuName = "Spell/Effect/Apply Strategy/Periodic Time")]
    public class PeriodicApplyStrategySO : EffectApplyStrategySO
    {
        [SerializeField] private float _period = 0f;

        public override EffectApplyStrategy CreateStrategy()
        {
            return new PeriodicApplyStrategy(this, _period);
        }
    }
}