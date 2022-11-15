using System.Collections;

using Unity.VisualScripting;

using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class PlayerSwordWeapon : Weapon
{
    [SerializeField] private float _maxAttackDistance;
    [SerializeField] private float _swordLength;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _force;
    [SerializeField] private float _damage;

    private Player _player;
    private LineRenderer _lr;

    private CheckArea _hitArea;

    private Coroutine _hitHolder;

    protected override void Awake()
    {
        base.Awake();
        _player = Inventory.GetComponentInParent<Player>();
        _lr = GetComponent<LineRenderer>();
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Vector2 attackAngle = _player.Input.LookAt - _player.Center;
        _hitArea = new CheckArea(_player.Center, _player.Center + attackAngle.normalized * _swordLength);

        collisions.Clear();
        collisions.AddRange(Physics2D.LinecastAll(_hitArea.a, _hitArea.b, whatIsTarget));

        foreach (var collision in collisions)
        {
            if (collision.collider.TryGetComponent<Physical>(out var physical))
            {
                physical.Push(_force, attackAngle);
            }

            if (collision.collider.TryGetComponent<Damageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }

        if (_hitHolder != null)
        {
            StopCoroutine(_hitHolder);
        }

        _hitHolder = StartCoroutine(DrawHit());
    }

    private IEnumerator DrawHit()
    {
        _lr.enabled = true;
        _lr.SetPosition(0, _hitArea.a);
        _lr.SetPosition(1, _hitArea.b);

        yield return new WaitUntil(() => ActiveTime > _attackSpeed);

        _lr.enabled = false;
        _player.AttackAbility.OnExit();
    }

    private void OnDrawGizmos()
    {
        Utility.DrawLine(_hitArea, collisions.Count > 0, Color.red);
    }
}
