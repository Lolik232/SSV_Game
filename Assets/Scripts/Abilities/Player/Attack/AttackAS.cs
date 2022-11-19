public class AttackAS : AbilityState<AttackAbility>
{
    private Inventory _inventory;

    protected override void Awake()
    {
        base.Awake();
        _inventory = GetComponentInChildren<Inventory>();
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        _inventory.Current.OnEnter();
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        _inventory.Current.OnExit();
    }
}
