using System.Collections.Generic;
using All.Interfaces;

namespace All.Spell
{
    public abstract class Spell : ILogicUpdate
    {
    
        protected readonly List<ISpellEffect> m_effects;

        protected readonly ILiveCycle m_liveCycle;

        public IReadOnlyList<ISpellEffect> Effects => m_effects.AsReadOnly();

        public ILiveCycle LiveCycle => m_liveCycle;
        
        public void LogicUpdate()
        {
            m_liveCycle.LogicUpdate();
        }

        public void ApplySpell(SpellHolder holder)
        {
            holder.AddSpell(this);
        }
    }
}