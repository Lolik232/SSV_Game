using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Physical), typeof(Rotateable), typeof(Damageable))]

[RequireComponent(typeof(DummyGroundedState), typeof(DummyInAirState))]

public class Dummy : Entity, IPhysical, IRotateable, IDamageable,
                             IGrounded
{
    private GroundChecker _groundChecker;

    private Physical _physical;
    private Rotateable _rotateable;
    private Damageable _damageable;

    public DummyGroundedState GroundedState
    {
        get;
        private set;
    }

    public DummyInAirState InAirState
    {
        get;
        private set;
    }

    public Vector2 Position => ((IPhysical)_physical).Position;

    public Vector2 Velocity => ((IPhysical)_physical).Velocity;

    public float Gravity => ((IPhysical)_physical).Gravity;

    public Vector2 Size => ((IPhysical)_physical).Size;

    public Vector2 Offset => ((IPhysical)_physical).Offset;

    public Vector2 Center => ((IPhysical)_physical).Center;

    public int FacingDirection => ((IRotateable)_rotateable).FacingDirection;

    public int BodyDirection => ((IRotateable)_rotateable).BodyDirection;

    public bool IsRotationLocked => ((IRotateable)_rotateable).IsRotationLocked;

    public float MaxHealth
    {
        get => ((IDamageable)_damageable).MaxHealth;
        set => ((IDamageable)_damageable).MaxHealth = value;
    }

    public float Health => ((IDamageable)_damageable).Health;

    public bool IsDead => ((IDamageable)_damageable).IsDead;

    public bool Grounded => ((IGrounded)_groundChecker).Grounded;

    public bool IsPushed => ((IPhysical)_physical).IsPushed;

    public void BlockRotation()
    {
        ((IRotateable)_rotateable).BlockRotation();
    }

    public void LookAt(Vector2 position)
    {
        ((IRotateable)_rotateable).LookAt(position);
    }

    public void OnDead()
    {
        ((IDamageable)_damageable).OnDead();
    }

    public IEnumerator Push(float force, Vector2 angle)
    {
        return ((IPhysical)_physical).Push(force, angle);
    }

    public void RestoreHealth(float regeneration)
    {
        ((IDamageable)_damageable).RestoreHealth(regeneration);
    }

    public void RotateBodyAt(Vector2 position)
    {
        ((IRotateable)_rotateable).RotateBodyAt(position);
    }

    public void RotateBodyIntoDirection(int direction)
    {
        ((IRotateable)_rotateable).RotateBodyIntoDirection(direction);
    }

    public void RotateIntoDirection(int direction)
    {
        ((IRotateable)_rotateable).RotateIntoDirection(direction);
    }

    public void TakeDamage(float damage, Vector2 attackPoint)
    {
        LookAt(attackPoint);
        ((IDamageable)_damageable).TakeDamage(damage, attackPoint);
    }

    public void UnlockRotation()
    {
        ((IRotateable)_rotateable).UnlockRotation();
    }

    protected override void Awake()
    {
        base.Awake();

        _groundChecker = GetComponent<GroundChecker>();

        _physical = GetComponent<Physical>();
        _rotateable = GetComponent<Rotateable>();
        _damageable = GetComponent<Damageable>();

        InAirState = GetComponent<DummyInAirState>();
        GroundedState = GetComponent<DummyGroundedState>();
    }

    private void Start()
    {
        RotateIntoDirection(1);
    }
}
