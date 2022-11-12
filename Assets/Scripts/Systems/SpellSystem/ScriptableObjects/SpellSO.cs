using System;
using System.Collections.Generic;
using System.Linq;
using All.BaseClasses;
using All.Interfaces;
using Extensions;
using Systems.SpellSystem.SpellEffect;
using Systems.SpellSystem.SpellEffect.SpellLiveCycle;
using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [CreateAssetMenu(menuName = "Spell/Spell", fileName = "Spell")]
    public class SpellSO : BaseDescriptionSO, IObject
    {
        private Sprite                  Icon;
        private ParticleSystem.Particle VFX;

        [Header("For editor setup")]
        [SerializeField] private string _name = "spell";

        [SerializeField] private LiveCycleSO _liveCycleSO;

        [SerializeField] private List<EffectSO>          _effectsSO = new();
        public                   IReadOnlyList<EffectSO> EffectsSO => _effectsSO;

        [Header("Initialized fields")]
        [SerializeField] private BaseLiveCycle _liveCycle;

        [SerializeField] private List<Effect> _effects = new();

        public void AddEffect(EffectSO effect)
        {
            _effects.Add(effect.CreateEffect());
        }

        public void InitializeSpell()
        {
            _liveCycle = _liveCycleSO.CreateLiveCycle();
            _effects.Clear();
            _effects = _effectsSO.Select(e => e.CreateEffect()).ToList();
            // _effectsSO.ForEach(e => _effects.Add(e.CreateEffect()));
        }

        public string Name => _name;

        public Spell CreateSpell()
        {
            return new Spell(this, _liveCycleSO.CreateLiveCycle(), _effects.Clone().ToArray());
        }
    }
}