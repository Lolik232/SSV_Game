using System.Collections.Generic;
using System.Linq;
using All.Interfaces;
using Systems.SpellSystem.SpellEffect.Actions;
using UnityEngine;

public class PlayerEffectCancelVisitor : MonoBehaviour, ISpellEffectActionCanceller
{
    private          Player                  _player;
    private readonly List<IBlockableBySpell> _blockedComponents = new();

    private void Awake()
    {
        _player = GetComponent<Player>();
        GetComponents(_blockedComponents);
    }

    public void Visit(DamageAction damageAction)
    {
        // nothing to action
    }

    public void Visit(BlockAbilityAction blockAbilityAction)
    {
        foreach (var blockedAbility in blockAbilityAction.AbilitiesToBlock)
        {
            foreach (var blockedComponent in
                     _blockedComponents
                         .Where(blockedComponent =>
                                    blockedComponent.Description.Equals(blockedAbility)))
            {
                UnlockComponent(blockedComponent);
            }
        }
    }

    private void UnlockComponent(IBlockable component)
    {
        if (_player != null && _player.enabled) component.Unlock();
    }
}