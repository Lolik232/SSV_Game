namespace All.Interfaces
{
    public interface ISpellEffectVisitor
    {
        // TODO: заменить данный метод Apply перегрузками для конкретных типов
        void Apply(ISpellEffect effect);
    }
} 