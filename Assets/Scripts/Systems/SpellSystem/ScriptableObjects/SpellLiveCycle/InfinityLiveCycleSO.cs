using Systems.SpellSystem.SpellLiveCycle;
using UnityEngine;

namespace Spells.SpellLiveCycle
{
    [CreateAssetMenu(fileName = "Spells/Live Cycle/Infinity", menuName = "livecycle")]
    public class InfinityLiveCycleSO : LiveCycleSO
    {
        public override BaseLiveCycle CreateLiveCycle()
        {
            return new InfinityLiveCycle();
        }
    }
}