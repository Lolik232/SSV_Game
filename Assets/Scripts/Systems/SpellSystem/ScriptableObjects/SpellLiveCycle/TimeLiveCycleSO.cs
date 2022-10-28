using Systems.SpellSystem.SpellLiveCycle;
using UnityEngine;

namespace Spells.SpellLiveCycle
{
    [CreateAssetMenu(fileName = "Spells/Live Cycle/Time", menuName = "livecycle")]

    public class TimeLiveCycleSO : LiveCycleSO
    {
        public override BaseLiveCycle CreateLiveCycle()
        {
            return new TimeLiveCycle();
        }
    }
}