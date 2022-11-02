using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(fileName = "Spell/Effect/Apply Strategy/Periodic Time", menuName = "apply strategy")]
    public class PeriodicApplyStrategySO : EffectApplyStrategySO
    {
        [SerializeField] private float _period = 0f;

        public override EffectApplyStrategy CreateStrategy()
        {
            return new PeriodicApplyStrategy(this, _period);
        }
    }
}