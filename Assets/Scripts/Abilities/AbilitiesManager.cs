using System.Collections.Generic;

public class AbilitiesManager : Component
{
    private readonly List<Ability> _abilities = new();

    private void Awake()
    {
        GetComponents(_abilities);
    }

    private void Update()
    {
        TryUseAbilities();
    }

    public void TryUseAbilities()
    {
        foreach (var ability in _abilities)
        {
            ability.OnEnter();
            ability.OnUpdate();
        }
    }
}