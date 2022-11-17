using UnityEngine;

namespace Systems.SpellSystem.SpellEffect.SpellLiveCycle
{
    [CreateAssetMenu(menuName = "Spell/Live Cycle/Infinity", fileName = "InfinityLiveCycle")]
    public class InfinityLiveCycleSO : LiveCycleSO
    {
        public override BaseLiveCycle CreateLiveCycle()
        {
            return new InfinityLiveCycle();
        }
    }
}