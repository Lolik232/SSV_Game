using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerSwordWeapon : PlayerWeapon
{
    [SerializeField] private float _swordLength;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _force;
    [SerializeField] private float _damage;



    protected override void Start()
    {
        SetAnimationSpeed("SwordAttack", _attackSpeed);
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();

        Player.LookAt(Player.Input.LookAt);
        Player.BlockRotation();

        collisions = new List<Collider2D>(Physics2D.OverlapCircleAll(Player.Center, _swordLength, whatIsTarget));

        StartCoroutine(WaitForEndOfAtack());
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Player.UnlockRotation();
    }

    private IEnumerator WaitForEndOfAtack()
    {
        yield return new WaitUntil(() => ActiveTime > _attackSpeed);

        OnHit(Player.Center, _force, _damage);
        Player.AttackAbility.OnExit();
    }

    private void OnDrawGizmos()
    {
        Utility.DrawCircle(attackPoint, _swordLength, collisions.Count > 0, Color.red);
    }
}
