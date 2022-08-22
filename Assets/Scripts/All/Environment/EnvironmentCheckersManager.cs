using System;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class EnvironmentCheckersManager : MonoBehaviour
{

    [SerializeField] private Transform m_GroundChecker;
    public GroundChecker GroundChecker { get; private set; }
    public BarrierChecker GroundIsCloseChecker { get; private set; }


    [SerializeField] private Transform m_WallChecker;
    public BarrierChecker WallChecker { get; private set; }
    public BarrierChecker WallBackChecker { get; private set; }

    [SerializeField] private Transform m_LedgeChecker;
    public BarrierChecker LedgeChecker { get; private set; }

    [SerializeField] private UnitData m_Data;

    private MoveController m_MoveController;

    private void Awake()
    {
        GroundChecker = new GroundChecker(m_GroundChecker, m_Data.groundCheckRadius, m_Data.whatIsGround);
        GroundIsCloseChecker = new BarrierChecker(m_GroundChecker, m_Data.groundIsCloseCheckDistance, Vector2.down, m_Data.whatIsGround);
        WallChecker = new BarrierChecker(m_WallChecker, m_Data.wallCheckDistance, Vector2.right, m_Data.whatIsGround);
        WallBackChecker = new BarrierChecker(m_WallChecker, m_Data.wallCheckDistance, Vector2.left, m_Data.whatIsGround);
        LedgeChecker = new BarrierChecker(m_LedgeChecker, m_Data.wallCheckDistance, Vector2.right, m_Data.whatIsGround);
    }

    private void Start()
    {
        m_MoveController = GetComponent<MoveController>();

        m_MoveController.FlipEvent += WallChecker.OnFlip;
        m_MoveController.FlipEvent += WallBackChecker.OnFlip;
        m_MoveController.FlipEvent += LedgeChecker.OnFlip;
    }

    private void FixedUpdate()
    {
        GroundChecker.CheckIfGrounded();
        GroundIsCloseChecker.CheckIfTouchingBarrier();
        WallChecker.CheckIfTouchingBarrier();
        WallBackChecker.CheckIfTouchingBarrier();
        LedgeChecker.CheckIfTouchingBarrier();
    }

    private void OnDestroy()
    {
        m_MoveController.FlipEvent -= WallChecker.OnFlip;
        m_MoveController.FlipEvent -= WallBackChecker.OnFlip;
        m_MoveController.FlipEvent -= LedgeChecker.OnFlip;
    }

    private void OnDrawGizmos()
    {
        GroundChecker.OnDrawGizmos();
        GroundIsCloseChecker.OnDrawGizmos();
        WallChecker.OnDrawGizmos();
        WallBackChecker.OnDrawGizmos();
        LedgeChecker.OnDrawGizmos();
    }
}
