using System.Collections;
using All.Interfaces;
using Systems.SpellSystem.SpellEffect.Actions;
using TestComponents;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WallChecker), typeof(GroundChecker), typeof(EdgeChecker))]
[RequireComponent(typeof(TargetChecker), typeof(AttackChecker))]
[RequireComponent(typeof(SkeletonWarriorGroundedState), typeof(SkeletonWarriorInAirState))]
[RequireComponent(typeof(Physical), typeof(Movable), typeof(Rotateable))]
[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(SkeletonWarriorBehaviour))]
[RequireComponent(typeof(MoveHorizontalAbility), typeof(AttackAbility))]
public class SkeletonWarrior : Entity, IPhysical, IMovable, IRotateable,
                               IGrounded, ITouchingWall, ITouchingEdge,
                               ITargetChecker, IAttackChecker
{
    private GroundChecker _groundChecker;
    private WallChecker   _wallChecker;
    private EdgeChecker   _edgeChecker;
    private TargetChecker _targetChecker;
    private AttackChecker _attackChecker;

    private Physical   _physical;
    private Movable    _movable;
    private Rotateable _rotateable;
    private Damageable _damageable;

    public SkeletonWarriorBehaviour Behaviour
    {
        get;
        private set;
    }
    public SkeletonWarriorGroundedState GroundedState
    {
        get;
        private set;
    }
    public SkeletonWarriorInAirState InAirState
    {
        get;
        private set;
    }
    public MoveHorizontalAbility MoveHorizontalAbility
    {
        get;
        private set;
    }
    public AttackAbility AttackAbility
    {
        get;
        private set;
    }
    public Inventory Inventory
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

    public bool IsPositionLocked => ((IMovable)_movable).IsPositionLocked;

    public bool IsVelocityLocked => ((IMovable)_movable).IsVelocityLocked || IsPushed;

    public float MaxHealth
    {
        get => ((IDamageable)_damageable).MaxHealth;
        set => ((IDamageable)_damageable).MaxHealth = value;
    }

    public float Health => ((IDamageable)_damageable).Health;

    public bool IsDead => ((IDamageable)_damageable).IsDead;

    public bool Grounded => ((IGrounded)_groundChecker).Grounded;

    public bool TouchingWall => ((ITouchingWall)_wallChecker).TouchingWall;

    public bool TouchingWallBack => ((ITouchingWall)_wallChecker).TouchingWallBack;

    public Vector2 WallPosition => ((ITouchingWall)_wallChecker).WallPosition;

    public int WallDirection => ((ITouchingWall)_wallChecker).WallDirection;

    public float YOffset => ((ITouchingWall)_wallChecker).YOffset;

    public bool TouchingEdge => ((ITouchingEdge)_edgeChecker).TouchingEdge;

    public bool IsPushed => ((IPhysical)_physical).IsPushed;

    public bool TargetDetected => ((ITargetChecker)_targetChecker).TargetDetected;

    public Vector2 TargetPosition => ((ITargetChecker)_targetChecker).TargetPosition;

    public float TargetDistance => ((ITargetChecker)_targetChecker).TargetDistance;

    public int TargetDirection => ((ITargetChecker)_targetChecker).TargetDirection;

    public bool AttackPermited => ((IAttackChecker)_attackChecker).AttackPermited;

    public void BlockPosition()
    {
        ((IMovable)_movable).BlockPosition();
    }

    public void BlockRotation()
    {
        ((IRotateable)_rotateable).BlockRotation();
    }

    public void BlockVelocity()
    {
        ((IMovable)_movable).BlockVelocity();
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

    public void ResetGravity()
    {
        ((IMovable)_movable).ResetGravity();
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

    public void SetGravity(float gravity)
    {
        ((IMovable)_movable).SetGravity(gravity);
    }

    public void SetPosition(Vector2 position)
    {
        ((IMovable)_movable).SetPosition(position);
    }

    public void SetPosition(float x, float y)
    {
        ((IMovable)_movable).SetPosition(x, y);
    }

    public void SetpositionX(float x)
    {
        ((IMovable)_movable).SetpositionX(x);
    }

    public void SetPositionY(float y)
    {
        ((IMovable)_movable).SetPositionY(y);
    }

    public void SetVelocity(Vector2 velocity)
    {
        ((IMovable)_movable).SetVelocity(velocity);
    }

    public void SetVelocity(float x, float y)
    {
        ((IMovable)_movable).SetVelocity(x, y);
    }

    public void SetVelocity(float speed, Vector2 angle, int direction)
    {
        ((IMovable)_movable).SetVelocity(speed, angle, direction);
    }

    public void SetVelocityX(float x)
    {
        ((IMovable)_movable).SetVelocityX(x);
    }

    public void SetVelocityY(float Y)
    {
        ((IMovable)_movable).SetVelocityY(Y);
    }

    // public void TakeDamage(float damage, Vector2 attackPoint)
    // {
    //     LookAt(attackPoint);
    //     ((IDamageable)_damageable).TakeDamage(damage, attackPoint);
    // }

    public void UnlockPosition()
    {
        ((IMovable)_movable).UnlockPosition();
    }

    public void UnlockRotation()
    {
        ((IRotateable)_rotateable).UnlockRotation();
    }

    public void UnlockVelocity()
    {
        ((IMovable)_movable).UnlockVelocity();
    }

    protected override void Awake()
    {
        base.Awake();

        _wallChecker   = GetComponent<WallChecker>();
        _edgeChecker   = GetComponent<EdgeChecker>();
        _groundChecker = GetComponent<GroundChecker>();
        _targetChecker = GetComponent<TargetChecker>();
        _attackChecker = GetComponent<AttackChecker>();

        _physical   = GetComponent<Physical>();
        _movable    = GetComponent<Movable>();
        _rotateable = GetComponent<Rotateable>();
        _damageable = GetComponent<Damageable>();

        Behaviour = GetComponent<SkeletonWarriorBehaviour>();

        GroundedState = GetComponent<SkeletonWarriorGroundedState>();
        InAirState    = GetComponent<SkeletonWarriorInAirState>();

        MoveHorizontalAbility = GetComponent<MoveHorizontalAbility>();
        AttackAbility         = GetComponent<AttackAbility>();

        Inventory = GetComponentInChildren<Inventory>();
    }

    private void Start()
    {
        RotateIntoDirection(-1);
    }
}