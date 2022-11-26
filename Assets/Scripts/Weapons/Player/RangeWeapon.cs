using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(LineRenderer))]

public class RangeWeapon : Weapon
{
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _range;
    [SerializeField] private float _attackSpeed;

    [SerializeField] private LayerMask _whatIsBarier;

    private List<Collider2D> _collisionsBuffer = new();

    private LineRenderer _lr;

    private CheckArea _ray;

    protected override void Awake()
    {
        base.Awake();
        _lr = GetComponent<LineRenderer>();
    }

    private void OnDisable()
    {
        _lr.enabled = false;
    }

    protected override void Start()
    {
        _lr.enabled = false;

        TextInfo ti = new CultureInfo("en-US",false).TextInfo;
        SetAnimationSpeed(ti.ToTitleCase(Name) + "Attack", Mathf.Max(_attackSpeed, 0.2f));
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        if (!Entity.IsRotationLocked)
        {
            Entity.LookAt(Entity.Behaviour.LookAt);
        }

        Vector2 attackDirection = (Entity.Behaviour.LookAt - Entity.Center).normalized;

        if (Entity.BodyDirection > 0 != attackDirection.x > 0)
        {
            Entity.AttackAbility.OnExit();
            return;
        }

        Entity.BlockRotation();
        collisions.Clear();

        RaycastHit2D hit = Physics2D.Raycast(Entity.Center, attackDirection, _range, _whatIsBarier);
        _ray = new(Entity.Center, hit ? hit.point : Entity.Center + attackDirection * _range);

        _lr.SetPosition(0, _ray.a);
        _lr.SetPosition(1, _ray.b);

        _collisionsBuffer = new List<Collider2D>(Physics2D.OverlapCircleAll(_ray.b, _radius, whatIsTarget));

        foreach (var collision in _collisionsBuffer)
        {
            if (!collisions.Contains(collision))
            {
                collisions.Add(collision);
                OnHit(transform.position, collision, 0f, _damage, false);
            }
        }

        StartCoroutine(WaitForEndOfAtack());
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        _lr.enabled = true;
        yield return new WaitForSeconds(0.1f);

        _lr.enabled = false;
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
        Utility.DrawLine(_ray, collisions.Count > 0, Color.red);
        Utility.DrawCircle(_ray.b, _radius, collisions.Count > 0, Color.red);
    }
}
