using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

[RequireComponent(typeof(Animator))]

public abstract class Weapon : ComponentBase
{
    [SerializeField] private string _name;
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] private Entity _entity;

    protected List<Collider2D> collisions = new();

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

    protected virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        Inventory = GetComponentInParent<Inventory>();
        OriginAnim = Inventory.GetComponentInParent<Animator>();
    }

    protected abstract void Start();

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
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        if (Name != string.Empty)
        {
            Anim.SetBool(Name, false);
            Debug.Log(Name);
        }
    }

    protected void SetAnimationSpeed(string clipName, float duration)
    {
        Utility.SetAnimationSpeed(OriginAnim, clipName, Name, duration);
    }
}
