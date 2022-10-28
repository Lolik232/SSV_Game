using Spells.Actions;

namespace All.Interfaces
{
    public interface ISpellEffectActionVisitor
    {
        void Visit(Damage damage);
        void Visit(BlockAbility blockAbility);
        // void Visit(Damage damage);
    }
}