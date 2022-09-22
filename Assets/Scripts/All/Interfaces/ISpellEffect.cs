namespace All.Interfaces
{
    public interface ISpellEffect : IObject
    {
        bool CanApply();

        void ApplyEffect(ISpellEffectVisitor effectApplier);
    }
}