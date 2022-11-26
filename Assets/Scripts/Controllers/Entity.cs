using System.Collections.Generic;

using Systems.SpellSystem.SpellEffect;

using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SpellHolder))]

public class Entity : MonoBehaviour
{
    public SpellHolder SpellHolder{
        get;
        private set;
    }

    private List<Component> _components = new();

    public AudioSource Source
    {
        get;
        private set;
    }

    protected List<IChecker> Checkers
    {
        get;
        private set;
    } = new();

    protected virtual void Awake()
    {
        GetComponents(Checkers);
        GetComponents(_components);
        Source = GetComponent<AudioSource>();
        SpellHolder = GetComponent<SpellHolder>();
    }

    private void FixedUpdate()
    {
        DoChecks();
    }

    private void OnEnable()
    {
        foreach (var component in _components)
        {
            component.enabled = true;
        }
    }

    private void OnDisable()
    {
        foreach (var component in _components)
        {
            component.enabled = false;
        }
    }

    public void UpdateCheckersPosition()
    {
        foreach (var checker in Checkers)
        {
            checker.UpdateCheckersPosition();
        }
    }

    public void DoChecks()
    {
        UpdateCheckersPosition();
        foreach (var checker in Checkers)
        {
            checker.DoChecks();
        }
    }
}
