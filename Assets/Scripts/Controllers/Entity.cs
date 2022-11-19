using System.Collections.Generic;

using UnityEngine;

public class Entity : MonoBehaviour
{
    protected List<IChecker> Checkers
    {
        get;
        private set;
    } = new();

    protected virtual void Awake()
    {
        GetComponents(Checkers);
    }

    private void FixedUpdate()
    {
        DoChecks();
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
