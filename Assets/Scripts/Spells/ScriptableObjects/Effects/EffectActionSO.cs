using All.BaseClasses;
using UnityEngine;

namespace Spells
{
    public abstract class EffectActionSO : BaseDescriptionSO
    {
        public abstract EffectAction CreateAction();
    }
}