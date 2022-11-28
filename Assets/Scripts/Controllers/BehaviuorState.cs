using System.Collections.Generic;

public class BehaviuorState<BehaviourControllerT> : ComponentBase where BehaviourControllerT : BehaviourController
{
    protected BehaviourControllerT Controller
    {
        get;
        private set;
    }
    public List<TransitionItem<BehaviuorState<BehaviourControllerT>>> Transitions
    {
        get;
        protected set;
    } = new();
    protected dynamic Entity
    {
        get;
        private set;
    }

    protected void Awake()
    {
        Controller = GetComponent<BehaviourControllerT>();
        Entity = GetComponent<Entity>();
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

        TryGetTransition();

        if (!IsActive)
        {
            return;
        }

        ApplyUpdateActions();
    }

    protected void TryGetTransition()
    {
        foreach (var transition in Transitions)
        {
            if (transition.condition())
            {
                transition.action?.Invoke();
                Controller.GetTransition(transition.target);
                return;
            }
        }
    }
}
