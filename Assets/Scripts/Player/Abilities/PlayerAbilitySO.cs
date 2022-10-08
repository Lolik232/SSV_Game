using System;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json.Bson;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;


public class PlayerAbilitySO : ScriptableObject
{
    [Header("Parameters")]
    [SerializeField] private PlayerAbilityStateSO _connectedState;

    [SerializeField] protected float duration;
    [SerializeField] private float _cooldown;
    protected float startTime;

    protected bool isAble;
    private bool _cachedIsAble;
    public bool IsActive { get; protected set; }

    protected List<UnityAction> useActions = new();
    protected List<UnityAction> updateActions = new();
    protected List<Func<bool>> conditions = new();

    private StateMachine _machine;
    protected Player Player { get; private set; }

    protected virtual void OnEnable()
    {
        isAble = false;
        IsActive = false;
        startTime = 0f;

        conditions = new List<Func<bool>> { () => isAble && Time.time > startTime + _cooldown };
        useActions = new List<UnityAction> { () => { startTime = Time.time; isAble = false; IsActive = true; } };
        updateActions.Clear();
    }

    public void Restore() => isAble = _cachedIsAble;
    public void Cache() => _cachedIsAble = isAble;
    public void Block() => isAble = false;
    public void Unlock() => isAble = true;
    public void SetAble(bool value) => isAble = value;
    public void Terminate() => IsActive = false;

    public void Initialize(Player player, StateMachine stateMachine)
    {
        Player = player;
        _machine = stateMachine;
    }

    public void TryUseAbility()
    {
        bool canUse = true;
        foreach (var condition in conditions)
        {
            canUse &= condition();
        }
        if (canUse)
        {
            foreach (var action in useActions) { action(); }
            _machine.GetTransitionState(_connectedState);
        }
    }

    public void OnUpdate()
    {
        foreach (var action in updateActions) { action(); }
    }
}
