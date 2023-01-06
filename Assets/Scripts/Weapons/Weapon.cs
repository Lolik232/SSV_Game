using System.Collections;
using System.Collections.Generic;
using All.Interfaces;
using SceneManagement;
using Systems.SpellSystem.SpellEffect;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public abstract class Weapon : ComponentBase
{
    public UnityEvent<Entity> EntityHit = default;

    private Coroutine _stanHolder;

    // protected SpellApplier SpellApplier
    // {
    //     get;
    //     private set;
    // }

    [SerializeField] private   string    _name;
    [SerializeField] protected LayerMask whatIsTarget;

    private Coroutine _exitTimeOutHolder;

    protected Vector2 attackPoint;

    protected HashSet<Collider2D> collisions = new();

    protected Inventory Inventory
    {
        get;
        private set;
    }

    protected Animator Anim
    {
        get;
        private set;
    }
    protected Animator OriginAnim
    {
        get;
        private set;
    }
    public string Name
    {
        get => _name;
    }
    protected dynamic Entity
    {
        get;
        private set;
    }

    protected virtual void Awake()
    {
        Anim         = GetComponent<Animator>();
        Inventory    = GetComponentInParent<Inventory>();
        Entity       = Inventory.GetComponentInParent<Entity>();
        OriginAnim   = Entity.GetComponent<Animator>();
        // SpellApplier = GetComponent<SpellApplier>();
    }

    protected abstract void Start();

    public void OnHit(Vector2    attackPoint,
                      Collider2D collider,
                      float      force,
                      float      damage,
                      bool       needPush = true)
    {
        this.attackPoint = attackPoint;

        if (collider.TryGetComponent<Entity>(out var entity))
        {
            if (needPush && entity is IPhysical physical)
            {
                entity.StartCoroutine(physical.Push(force, physical.Center - this.attackPoint));
            }

            if (entity.TryGetComponent<Damageable>(out var damageable))
            {
                if (!damageable.IsDead)
                {
                    damageable.TakeDamage(damage, attackPoint);
                }
            }

            EntityHit?.Invoke(entity);
        }
    }


    public override void OnEnter()
    {
        if (IsActive)
        {
            return;
        }

        ApplyEnterActions();
    }

    public override void OnExit()
    {
        if (!IsActive)
        {
            return;
        }

        ApplyExitActions();
    }

    public override void OnUpdate()
    {
        if (!IsActive)
        {
            return;
        }

        ApplyUpdateActions();
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        if (Name != string.Empty)
        {
            Anim.SetBool(Name, true);
            OriginAnim.SetTrigger(Name);
            Debug.Log(Name);
        }

        if (_exitTimeOutHolder != null)
        {
            Entity.UnlockRotation();
            StopCoroutine(_exitTimeOutHolder);
        }

        if (!Entity.IsRotationLocked)
        {
            Entity.LookAt(Entity.Behaviour.LookAt);
        }

        Entity.BlockRotation();
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        _exitTimeOutHolder = StartCoroutine(ExitTineOut());
    }

    private IEnumerator ExitTineOut()
    {
        yield return new WaitUntil(() => ActiveTime > 0.2f);

        _exitTimeOutHolder = null;
        if (Name != string.Empty)
        {
            Anim.SetBool(Name, false);
            Debug.Log(Name);
        }

        Entity.UnlockRotation();
    }

    protected void SetAnimationSpeed(string clipName, float duration)
    {
        Utility.SetAnimationSpeed(OriginAnim, clipName, Name, duration);
    }

    private IEnumerator StanTimeOut(Player player)
    {
        yield return new WaitForSeconds(0.5f);
        if (player != null && player.enabled)
        {
            player.Behaviour.Unlock();

            _stanHolder = null;
        }
    }
}