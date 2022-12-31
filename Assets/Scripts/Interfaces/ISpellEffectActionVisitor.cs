using Systems.SpellSystem.SpellEffect.Actions;

namespace All.Interfaces
{
    public interface ISpellEffectActionVisitor
    {
        void Visit(DamageAction       damageAction);
        void Visit(BlockAbilityAction blockAbilityAction);
    }

    /// <summary>
    /// Empty interface for identification
    /// </summary>
    public interface ISpellEffectActionApplier : ISpellEffectActionVisitor
    {
    }

    /// <summary>
    /// Empty interface for identification
    /// </summary>
    public interface ISpellEffectActionCanceller : ISpellEffectActionVisitor
    {
    }
}