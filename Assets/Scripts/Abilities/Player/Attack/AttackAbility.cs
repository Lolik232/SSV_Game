using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(AttackAS))]

public class AttackAbility : Ability
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _manaCost;

    private Coroutine _manaRegenTimeOutHolder;
    private Coroutine _wallClimbTimeOutHolder;

    public bool CanWallClimb
    {
        get;
        private set;
    } = true;

    public AttackAS Attack
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Default = Attack = GetComponent<AttackAS>();

        GetAbilityStates<AttackAbility>();
    }

    protected override void Start()
    {
        base.Start();
        enterConditions.Add(() => Entity.Behaviour.Attack && InactiveTime > _cooldown && (Entity is not IPower || Entity.Mana >= _manaCost));
        exitConditions.Add(() => false);
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        if (Entity is IPower)
        {
            if (_manaRegenTimeOutHolder != null)
            {
                StopCoroutine(_manaRegenTimeOutHolder);
            }
            else
            {
                Entity.BlockManaRegen();
            }

            Entity.UseMana(_manaCost);
        }

        if (_wallClimbTimeOutHolder != null)
        {
            StopCoroutine(_wallClimbTimeOutHolder);
        }
        else
        {
            CanWallClimb = false;
        }
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.Behaviour.Attack = false;

        if (Entity is IPower)
        {
            _manaRegenTimeOutHolder = StartCoroutine(ManaRegenTimeOut());
        }

        _wallClimbTimeOutHolder = StartCoroutine(WallClimbTimeOut());
    }

    private IEnumerator WallClimbTimeOut()
    {
        yield return new WaitForSeconds(0.2f);

        CanWallClimb = true;
        _wallClimbTimeOutHolder = null;
    }

    private IEnumerator ManaRegenTimeOut()
    {
        yield return new WaitForSeconds(0.5f);

        Entity.UnlockManaRegen();
        _manaRegenTimeOutHolder = null;
    }
}
