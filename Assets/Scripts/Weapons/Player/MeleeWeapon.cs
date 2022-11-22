using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float _length;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _force;
    [SerializeField] private float _damage;

    public float Length
    {
        get => _length;
    }

    public float AttackSpeed
    {
        get => _attackSpeed;
    }

    protected override void Start()
    {
        TextInfo ti = new CultureInfo("en-US",false).TextInfo;
        SetAnimationSpeed(ti.ToTitleCase(Name) + "Attack", _attackSpeed);
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();

        if (!Entity.IsRotationLocked)
        {
            Entity.LookAt(Entity.Behaviour.LookAt);
        }

        Entity.BlockRotation();

        collisions = new List<Collider2D>(Physics2D.OverlapCircleAll(Entity.Center, _length, whatIsTarget));

        OnHit(Entity.Center, _force, _damage);
        StartCoroutine(WaitForEndOfAtack());
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.UnlockRotation();
    }

    private IEnumerator WaitForEndOfAtack()
    {
        yield return new WaitUntil(() => ActiveTime > _attackSpeed);

        Entity.AttackAbility.OnExit();
    }

    private void OnDrawGizmos()
    {
        Utility.DrawCircle(attackPoint, _length, collisions.Count > 0, Color.red);
    }
}
