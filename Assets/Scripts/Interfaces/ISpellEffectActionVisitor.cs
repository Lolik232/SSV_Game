using Systems.SpellSystem.SpellEffect.Actions;

namespace All.Interfaces
{
    public interface ISpellEffectActionVisitor
    {
        void Visit(DamageAction damageAction);
        void Visit(BlockAbilityAction blockAbilityAction);
        // void Visit(Damage damage);
    }
}