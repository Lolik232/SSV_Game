using System.Collections.Generic;
using System.Linq;
using All.BaseClasses;
using All.Interfaces;
using Extensions;
using Systems.SpellSystem.SpellEffect.SpellLiveCycle;
using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [CreateAssetMenu(menuName = "Spell/Spell", fileName = "Spell")]
    public class SpellSO : BaseDescriptionSO, IObject
    {
        [SerializeField] private Sprite                  Icon;
        [SerializeField] private ParticleSystem.Particle VFX;

        [Header("For editor setup")]
        [SerializeField] private string _name = "spell";

        [SerializeField] private LiveCycleSO _liveCycleSO;

        [SerializeField] private List<EffectSO>          _effectsSO = new();
        public                   IReadOnlyList<EffectSO> EffectsSO => _effectsSO;

        public string Name => _name;

        public Spell CreateSpell()
        {
            var createdEffects = _effectsSO.Select(e => e.CreateEffect()).ToArray();
            return new Spell(this,
                             _liveCycleSO.CreateLiveCycle(),
                             createdEffects
                            );
        }
    }
}