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

    public Vector2 Position => _physical.Position;

    public Vector2 Velocity => _physical.Velocity;

    public float Gravity => _physical.Gravity;

    public Vector2 Size => _physical.Size;

    public Vector2 Offset => _physical.Offset;

    public Vector2 Center => _physical.Center;

    public int FacingDirection => _rotateable.FacingDirection;

    public int BodyDirection => _rotateable.BodyDirection;

    public float MaxHealth
    {
        get => _damageable.MaxHealth;
        set => _damageable.MaxHealth = value;
    }

    public float Health => _damageable.Health;

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

    public bool Grounded => _groundChecker.Grounded;

    public bool IsDead => _damageable.IsDead;

    public void LookAt(Vector2 position)
    {
        _rotateable.LookAt(position);
    }

    public void Push(float force, Vector2 angle)
    {
        _physical.Push(force, angle);
    }

    public void RestoreHealth(float regeneration)
    {
        _damageable.RestoreHealth(regeneration);
    }

    public void RotateBodyIntoDirection(int direction)
    {
        _rotateable.RotateBodyIntoDirection(direction);
    }

    public void RotateIntoDirection(int direction)
    {
        _rotateable.RotateIntoDirection(direction);
    }

    public void TakeDamage(float damage, Entity damager)
    {
        LookAt(damager.transform.position);
        _damageable.TakeDamage(damage, damager);
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
