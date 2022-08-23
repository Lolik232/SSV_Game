using System;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class EnvironmentCheckersManager : MonoBehaviour
{

    [SerializeField] private Transform m_GroundChecker;
    public GroundChecker GroundChecker { get; private set; }
    public BarrierChecker GroundCloseChecker { get; private set; }


    [SerializeField] private Transform m_WallChecker;
    public BarrierChecker WallChecker { get; private set; }
    public BarrierChecker WallBackChecker { get; private set; }

    [SerializeField] private Transform m_LedgeChecker;
    public BarrierChecker LedgeChecker { get; private set; }

    [SerializeField] private UnitData m_Data;
    public UnitData Data { get => m_Data; private set => m_Data = value; }

    public MoveController MoveController { get; private set; }

    protected virtual void Awake()
    {
        GroundChecker = new GroundChecker(m_GroundChecker, m_Data.groundCheckRadius, m_Data.whatIsGround);
        GroundCloseChecker = new BarrierChecker(m_GroundChecker, m_Data.groundIsCloseCheckDistance, Vector2.down, m_Data.whatIsGround);
        WallChecker = new BarrierChecker(m_WallChecker, m_Data.wallCheckDistance, Vector2.right, m_Data.whatIsGround);
        WallBackChecker = new BarrierChecker(m_WallChecker, m_Data.wallCheckDistance, Vector2.left, m_Data.whatIsGround);
        LedgeChecker = new BarrierChecker(m_LedgeChecker, m_Data.wallCheckDistance, Vector2.right, m_Data.whatIsGround);
    }

    protected virtual void Start()
    {
        MoveController = GetComponent<MoveController>();

        MoveController.FlipEvent += WallChecker.OnFlip;
        MoveController.FlipEvent += WallBackChecker.OnFlip;
        MoveController.FlipEvent += LedgeChecker.OnFlip;
    }

    protected virtual void FixedUpdate()
    {
        GroundChecker.CheckIfGrounded();
        GroundCloseChecker.CheckIfTouchingBarrier();
        WallChecker.CheckIfTouchingBarrier();
        WallBackChecker.CheckIfTouchingBarrier();
        LedgeChecker.CheckIfTouchingBarrier();
    }

    protected virtual void OnDestroy()
    {
        MoveController.FlipEvent -= WallChecker.OnFlip;
        MoveController.FlipEvent -= WallBackChecker.OnFlip;
        MoveController.FlipEvent -= LedgeChecker.OnFlip;
    }

    protected virtual void OnDrawGizmos()
    {
        GroundChecker?.OnDrawGizmos();
        GroundCloseChecker?.OnDrawGizmos();
        WallChecker?.OnDrawGizmos();
        WallBackChecker?.OnDrawGizmos();
        LedgeChecker?.OnDrawGizmos();
    }
}
