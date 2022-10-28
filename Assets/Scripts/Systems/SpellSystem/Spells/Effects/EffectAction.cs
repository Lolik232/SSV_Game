using All.Interfaces;

namespace Spells
{
    public abstract class EffectAction
    {
        private EffectActionSO _effectActionSO;
        public  EffectActionSO EffectActionSO => _effectActionSO;

        protected EffectAction(EffectActionSO effectActionSo)
        {
            _effectActionSO = effectActionSo;
        }

        public abstract void Apply(ISpellEffectActionVisitor visitor);
    }
}