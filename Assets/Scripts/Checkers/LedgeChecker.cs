using UnityEngine;

[RequireComponent(typeof(Rotateable), typeof(Physical), typeof(WallChecker))]

public class LedgeChecker : MonoBehaviour, ITouchingLedge, IChecker
{
    [SerializeField] private LayerMask _whatIsTarget;
    [SerializeField] private float _ledgeCheckDistance;
    [SerializeField] private float _ledgeCheckerYOffset;
    [SerializeField] private PickableColor _color;

    private CheckArea _ledgeCheckRay;
    private CheckArea _groundCheckRay;

    private Physical _physical;
    private Rotateable _rotateable;
    private WallChecker _wallChecker;

    public bool TouchingLedge
    {
        get; private set;
    }
    public Vector2 GroundPosition
    {
        get; private set;
    }
    public bool TouchingGround
    {
        get; private set;
    }
    public float YOffset
    {
        get => _ledgeCheckerYOffset;
    }

    private void Awake()
    {
        _physical = GetComponent<Physical>();
        _rotateable = GetComponent<Rotateable>();
        _wallChecker = GetComponent<WallChecker>();
    }

    private void OnDrawGizmos()
    {
        Utility.DrawArea(_ledgeCheckRay, TouchingLedge, _color.Color);
        Utility.DrawArea(_groundCheckRay, TouchingGround, _color.Color);
    }

    public void DoChecks()
    {
        TouchingLedge = Physics2D.Linecast(_ledgeCheckRay.a, _ledgeCheckRay.b, _whatIsTarget);
        RaycastHit2D hit =  Physics2D.Linecast(_groundCheckRay.a, _groundCheckRay.b, _whatIsTarget);
        TouchingGround = hit;
        if (TouchingGround)
        {
            GroundPosition = hit.point;
        }
    }

    public void UpdateCheckersPosition()
    {
        Vector2 checkerOffset = _physical.Size / 2 - Vector2.one * IChecker.CHECK_OFFSET;
        float rightCheckerPositionX = _physical.Center.x + _rotateable.FacingDirection * checkerOffset.x;

        Vector2 workspace = new(rightCheckerPositionX, _physical.Position.y + YOffset);
        _ledgeCheckRay = new CheckArea(workspace.x,
                                        workspace.y,
                                        workspace.x + _rotateable.FacingDirection * _ledgeCheckDistance,
                                        workspace.y);
        workspace.x += _rotateable.FacingDirection * _ledgeCheckDistance;
        _groundCheckRay = new CheckArea(workspace.x,
                                        workspace.y,
                                        workspace.x,
                                        workspace.y - (YOffset - _wallChecker.YOffset + IChecker.CHECK_OFFSET));
    }
}

