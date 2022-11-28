using System.Collections.Generic;
using System.Linq;

public class BehaviourController : Component, IBlockableBySpell
{
    public AbilitySO description;

    private Blocker _blocker = new();

    private readonly List<dynamic> _states = new();

    public dynamic Entity
    {
        get;
        private set;
    }

    public dynamic Current
    {
        get;
        private set;
    }

    public AbilitySO Description
    {
        get => description;
    }

    public bool IsLocked
    {
        get => _blocker.IsLocked;
    }

    public virtual void Block()
    {
        _blocker.AddBlock();
    }

    public virtual void Unlock()
    {
        _blocker.RemoveBlock();
    }

    protected virtual void Awake()
    {
        Entity = GetComponent<Entity>();
    }

    protected virtual void Start()
    {
        Initialize(_states.First());
    }

    protected virtual void Update()
    {
        if(IsLocked) return;
        Current.OnUpdate();
    }

    private void Initialize(dynamic target)
    {
        Current = target;
        Current.OnEnter();
    }

    public void GetTransition(dynamic target)
    {
        if (!IsLocked)
        {
            Current.OnExit();
            Current = target;
            Current.OnEnter();
        }
    }

    protected void GetBehaviourStates<BehaviourT>() where BehaviourT : BehaviourController
    {
        List<BehaviuorState<BehaviourT>> states  = new();
        GetComponents(states);
        foreach (var state in states)
        {
            _states.Add(state);
        }
    }
    

}
