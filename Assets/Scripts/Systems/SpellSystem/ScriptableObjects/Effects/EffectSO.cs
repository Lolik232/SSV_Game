using System;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [CreateAssetMenu(menuName = "Spell/Effect/Effect", fileName = "Effect")]
    [Serializable]
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