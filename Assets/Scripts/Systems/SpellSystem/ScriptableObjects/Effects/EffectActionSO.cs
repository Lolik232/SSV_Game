using All.BaseClasses;

namespace Systems.SpellSystem.SpellEffect
{
    public abstract class EffectActionSO : BaseDescriptionSO
    {
        public abstract EffectAction CreateAction();
    }
}