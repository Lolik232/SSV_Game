using UnityEngine;

[RequireComponent(typeof(Rotateable), typeof(Physical))]

public class EdgeChecker : Component, ITouchingEdge, IChecker
{
    [SerializeField] private LayerMask _whatIsTarget;
    [SerializeField] private float _edgeCheckDistance;
    [SerializeField] private PickableColor _color;

    private CheckArea _edgeCheckRay;

    private Physical _physical;
    private Rotateable _rotateable;

    public bool TouchingEdge
    {
        get;
        private set;
    }

    private void Awake()
    {
        _physical = GetComponent<Physical>();
        _rotateable = GetComponent<Rotateable>();
    }

    public void DoChecks()
    {
        TouchingEdge = Physics2D.Linecast(_edgeCheckRay.a, _edgeCheckRay.b, _whatIsTarget);
    }

    public void UpdateCheckersPosition()
    {
        Vector2 checkerOffset = _physical.Size / 2 - Vector2.one * IChecker.CHECK_OFFSET;
        float rightCheckerPositionX = _physical.Center.x + _rotateable.FacingDirection * checkerOffset.x;

        Vector2 workspace = new(rightCheckerPositionX, _physical.Position.y + IChecker.CHECK_OFFSET);
        _edgeCheckRay = new CheckArea(workspace.x,
                                    workspace.y,
                                    workspace.x,
                                    workspace.y - _edgeCheckDistance - IChecker.CHECK_OFFSET);
    }

    private void OnDrawGizmos()
    {
        Utility.DrawLine(_edgeCheckRay, TouchingEdge, _color.Color);
    }
}
