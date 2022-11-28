using System.Collections;

using UnityEngine;

public class AttackAS : AbilityState<AttackAbility>
{
    private Inventory _inventory;

    protected override void Awake()
    {
        base.Awake();
        _inventory = GetComponentInChildren<Inventory>();
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        StartCoroutine(EnterTimeOut());   
    }

    private IEnumerator EnterTimeOut()
    {
        yield return null;
        yield return new WaitForEndOfFrame();

        _inventory.Current.OnEnter();

        if (_clip != null)
        {
            Entity.Source.PlayOneShot(_clip);
        }
    }

    protected override void ApplyUpdateActions()
    {
        base.ApplyUpdateActions();
        _inventory.Current.OnUpdate();
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        _inventory.Current.OnExit();
    }
}