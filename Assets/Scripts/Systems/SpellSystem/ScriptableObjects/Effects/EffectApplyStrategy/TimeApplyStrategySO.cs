using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [CreateAssetMenu(menuName = "Spell/Effect/Apply Strategy/Time")]
    public class TimeApplyStrategySO : EffectApplyStrategySO
    {
        [SerializeField] private float _time = 0f;

        public override EffectApplyStrategy CreateStrategy()
        {
            return new TimeApplyStrategy(this, _time);
        }
    }
}