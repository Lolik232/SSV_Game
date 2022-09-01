using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class Unit : MonoBehaviour
{
    public EnvironmentCheckersManager EnvironmentCheckersManager { get; private set; }
    public Rigidbody2D RB { get; private set; }

    public MoveController MoveController { get; protected set; }

    [SerializeField] protected UnitData Data;

    [SerializeField] private Transform m_GroundChecker;
    [SerializeField] private Transform m_WallChecker;
    [SerializeField] private Transform m_LedgeChecker;
    protected virtual void Awake()
    {
        RB = GetComponent<Rigidbody2D>();

        EnvironmentCheckersManager = new EnvironmentCheckersManager(m_GroundChecker, m_WallChecker, m_LedgeChecker, this, Data);
    }

    protected virtual void Start()
    {
        EnvironmentCheckersManager.Initialize();
        MoveController.Initialize();
    }

    protected virtual void Update()
    {
        MoveController.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        EnvironmentCheckersManager.PhysicsUpdate();
    }

    protected virtual void OnDrawGizmos()
    {
    }
}
