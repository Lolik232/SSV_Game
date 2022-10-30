using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(menuName = "Spell/Effect", fileName = "effect")]
    public class EffectSO : ScriptableObject
    {
        [SerializeField] private EffectActionSO        _effectActionSO;
        [SerializeField] private EffectApplyStrategySO _effectApplyStrategyS0;

        public Effect CreateEffect()
        {
            return new Effect(this,
                              _effectApplyStrategyS0.CreateStrategy(),
                              _effectActionSO.CreateAction()
                             );
        }
    }
}