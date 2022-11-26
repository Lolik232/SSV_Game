using System.Collections;
using System.Collections.Generic;

using All.Interfaces;

using Systems.SpellSystem.SpellEffect.Actions;

using Unity.VisualScripting;

using UnityEngine;

public class PlayerEffectApplyVisitor : MonoBehaviour, ISpellEffectActionVisitor
{
    private Player _player;
    private List<IBlockableBySpell> _blockedComponents = new();

    private void Awake()
    {
        _player = GetComponent<Player>();
        GetComponents(_blockedComponents);
    }

    public void Visit(DamageAction damageAction)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(BlockAbilityAction blockAbilityAction)
    {
        foreach (var blockedAbility in blockAbilityAction.AbilitiesToBlock)
        {
            foreach (var blockedComponent in _blockedComponents)
            {
                if (blockedComponent.Description == blockedAbility)
                {
                    StartCoroutine(UnlockTimeOut(blockedComponent));
                }
            }
        }
    }

    private IEnumerator UnlockTimeOut(IBlockableBySpell component)
    {
        component.Block();

        yield return new WaitForSeconds(0.5f);

        component.Unlock();
    }
}
