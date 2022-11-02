using System.Collections.Generic;
using All.Interfaces;
using Spells;
using Systems.SpellSystem.SpellLiveCycle;

namespace Systems.SpellSystem.Spells
{
    public class Spell : ILogicUpdate
    {
        private List<Effect>  _effects = new();
        private BaseLiveCycle _liveCycle;

        public bool IsEnd => _liveCycle.IsEnd();

        public void ApplyEffects(ISpellEffectActionVisitor visitor)
        {
            foreach (var effect in _effects)
            {
                effect.Apply(visitor);
            }
        }

        public void LogicUpdate()
        {
            _liveCycle.LogicUpdate();
        }
    }
}