using System.Collections;

using All.Events;
using All.Interfaces;

using Systems.SpellSystem.SpellEffect.Actions;

using UnityEngine;

[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(WallChecker), typeof(GroundChecker), typeof(CeilChecker))]
[RequireComponent(typeof(LedgeChecker))]

[RequireComponent(typeof(Physical), typeof(Movable), typeof(Crouchable))]
[RequireComponent(typeof(Rotateable), typeof(Damageable), typeof(Power))]

[RequireComponent(typeof(PlayerInputReader))]

[RequireComponent(typeof(PlayerGroundedState), typeof(PlayerInAirState), typeof(PlayerTouchingWallState))]
[RequireComponent(typeof(PlayerOnLedgeState))]

[RequireComponent(typeof(MoveHorizontalAbility), typeof(MoveOnWallAbility))]
[RequireComponent(typeof(LedgeClimbAbility))]
[RequireComponent(typeof(CrouchAbility))]
[RequireComponent(typeof(JumpAbility))]
[RequireComponent(typeof(DashAbility))]
[RequireComponent(typeof(AttackAbility))]

[RequireComponent(typeof(PlayerEffectApplyVisitor))]

public class Player : Entity, IPhysical, IMovable, ICrouchable, IRotateable,
                              IGrounded, ITouchingWall, ITouchingCeiling, ITouchingLedge, IDamageable, IPower, ISpellEffectActionVisitor
{
    [SerializeField] private VoidEventChannelSO _playerDiedChannel = default;
    [SerializeField] private AudioClip _hitSound;

    private WallChecker _wallChecker;
    private GroundChecker _groundChecker;
    private CeilChecker _ceilChecker;
    private LedgeChecker _ledgeChecker;

    private Physical _physical;
    private Movable _movable;
    private Crouchable _crouchable;
    private Rotateable _rotateable;
    private Damageable _damageable;
    private Power _power;

    public PlayerInputReader Behaviour
    {
        get;
        private set;
    }

    public PlayerGroundedState GroundedState
    {
        get;
        private set;
    }
    public PlayerInAirState InAirState
    {
        get;
        private set;
    }
    public PlayerTouchingWallState TouchingWallState
    {
        get;
        private set;
    }
    public PlayerOnLedgeState OnLedgeState
    {
        get;
        private set;
    }

    public MoveHorizontalAbility MoveHorizontalAbility
    {
        get;
        private set;
    }
    public MoveOnWallAbility MoveVerticalAbility
    {
        get;
        private set;
    }
    public JumpAbility JumpAbility
    {
        get;
        private set;
    }
    public CrouchAbility CrouchAbility
    {
        get;
        private set;
    }
    public LedgeClimbAbility LedgeClimbAbility
    {
        get;
        private set;
    }

    public DashAbility DashAbility
    {
        get;
        private set;
    }

    public AttackAbility AttackAbility
    {
        get;
        private set;
    }

    public PlayerEffectApplyVisitor PlayerEffectApplyVisitor
    {
        get; private set;
    }

    public Vector2 Position => ((IPhysical)_physical).Position;

    public Vector2 Velocity => ((IPhysical)_physical).Velocity;

    public float Gravity => ((IPhysical)_physical).Gravity;

    public Vector2 Size => ((IPhysical)_physical).Size;

    public Vector2 Offset => ((IPhysical)_physical).Offset;

    public Vector2 Center => ((IPhysical)_physical).Center;

    public bool IsPositionLocked => ((IMovable)_movable).IsPositionLocked;

    public bool IsVelocityLocked => ((IMovable)_movable).IsVelocityLocked || IsPushed;

    public Vector2 StandSize => ((ICrouchable)_crouchable).StandSize;

    public Vector2 StandOffset => ((ICrouchable)_crouchable).StandOffset;

    public Vector2 StandCenter => ((ICrouchable)_crouchable).StandCenter;

    public Vector2 CrouchSize => ((ICrouchable)_crouchable).CrouchSize;

    public Vector2 CrouchOffset => ((ICrouchable)_crouchable).CrouchOffset;

    public Vector2 CrouchCenter => ((ICrouchable)_crouchable).CrouchCenter;

    public bool IsStanding => ((ICrouchable)_crouchable).IsStanding;

    public int FacingDirection => ((IRotateable)_rotateable).FacingDirection;

    public int BodyDirection => ((IRotateable)_rotateable).BodyDirection;

    public bool IsRotationLocked => ((IRotateable)_rotateable).IsRotationLocked;

    public bool Grounded => ((IGrounded)_groundChecker).Grounded;

    public bool TouchingWall => ((ITouchingWall)_wallChecker).TouchingWall;

    public bool TouchingWallBack => ((ITouchingWall)_wallChecker).TouchingWallBack;

    public Vector2 WallPosition => ((ITouchingWall)_wallChecker).WallPosition;

    public int WallDirection => ((ITouchingWall)_wallChecker).WallDirection;

    public float YOffset => ((ITouchingWall)_wallChecker).YOffset;

    public bool TouchingCeiling => ((ITouchingCeiling)_ceilChecker).TouchingCeiling;

    public bool TouchingLedge => ((ITouchingLedge)_ledgeChecker).TouchingLedge;

    public bool TouchingGround => ((ITouchingLedge)_ledgeChecker).TouchingGround;

    public Vector2 GroundPosition => ((ITouchingLedge)_ledgeChecker).GroundPosition;

    public bool IsPushed => ((IPhysical)_physical).IsPushed;

    public float MaxHealth
    {
        get => ((IDamageable)_damageable).MaxHealth;
        set => ((IDamageable)_damageable).MaxHealth = value;
    }

    public float Health => ((IDamageable)_damageable).Health;

    public bool IsDead => ((IDamageable)_damageable).IsDead;

    public float MaxMana
    {
        get => ((IPower)_power).MaxMana;
        set => ((IPower)_power).MaxMana = value;
    }

    public float Mana => ((IPower)_power).Mana;

    public bool ManaRegenBlocked => ((IPower)_power).ManaRegenBlocked;

    public float ManaRegeneration => ((IPower)_power).ManaRegeneration;

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

    public void Crouch()
    {
        ((ICrouchable)_crouchable).Crouch();
    }

    public void LookAt(Vector2 position)
    {
        ((IRotateable)_rotateable).LookAt(position);
    }

    public void OnDead()
    {
        ((IDamageable)_damageable).OnDead();
        _playerDiedChannel.RaiseEvent();

        StartCoroutine(AfterDeadTimeOut());
    }

    private IEnumerator AfterDeadTimeOut()
    {
        yield return new WaitForSeconds(2f);
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

    public void Stand()
    {
        ((ICrouchable)_crouchable).Stand();
    }

    public void TakeDamage(float damage, Vector2 attackPoint)
    {
        LookAt(attackPoint);

        if (_hitSound != null)
        {
            Source.PlayOneShot(_hitSound, 1f);
        }

        ((IDamageable)_damageable).TakeDamage(damage, attackPoint);
    }

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

        _wallChecker = GetComponent<WallChecker>();
        _ledgeChecker = GetComponent<LedgeChecker>();
        _ceilChecker = GetComponent<CeilChecker>();
        _groundChecker = GetComponent<GroundChecker>();

        _physical = GetComponent<Physical>();
        _movable = GetComponent<Movable>();
        _rotateable = GetComponent<Rotateable>();
        _crouchable = GetComponent<Crouchable>();
        _damageable = GetComponent<Damageable>();
        _power = GetComponent<Power>();

        Behaviour = GetComponent<PlayerInputReader>();

        GroundedState = GetComponent<PlayerGroundedState>();
        InAirState = GetComponent<PlayerInAirState>();
        TouchingWallState = GetComponent<PlayerTouchingWallState>();
        OnLedgeState = GetComponent<PlayerOnLedgeState>();

        MoveHorizontalAbility = GetComponent<MoveHorizontalAbility>();
        MoveVerticalAbility = GetComponent<MoveOnWallAbility>();
        JumpAbility = GetComponent<JumpAbility>();
        CrouchAbility = GetComponent<CrouchAbility>();
        LedgeClimbAbility = GetComponent<LedgeClimbAbility>();
        DashAbility = GetComponent<DashAbility>();
        AttackAbility = GetComponent<AttackAbility>();

        PlayerEffectApplyVisitor = GetComponent<PlayerEffectApplyVisitor>();
    }

    private void Update()
    {
        if (!ManaRegenBlocked)
        {
            RestoreMana(ManaRegeneration * Time.deltaTime);
        }
    }

    private void Start()
    {
        RotateIntoDirection(1);
        Stand();
    }

    public void UseMana(float cost)
    {
        ((IPower)_power).UseMana(cost);
    }

    public void RestoreMana(float regeneration)
    {
        ((IPower)_power).RestoreMana(regeneration);
    }

    public void BlockManaRegen()
    {
        ((IPower)_power).BlockManaRegen();
    }

    public void UnlockManaRegen()
    {
        ((IPower)_power).UnlockManaRegen();
    }

    // TODO: remove
    public void Visit(DamageAction damageAction)
    {
        ((ISpellEffectActionVisitor)PlayerEffectApplyVisitor).Visit(damageAction);
    }

    public void Visit(BlockAbilityAction blockAbilityAction)
    {
        ((ISpellEffectActionVisitor)PlayerEffectApplyVisitor).Visit(blockAbilityAction);
    }
    
    // end remove
}
