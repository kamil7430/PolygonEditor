using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;
using System.Numerics;

namespace PolygonEditor.Model.EdgeConstraints;

public class DiagonalEdgeConstraint : IEdgeConstraint
{
    public enum DiagonalDirection
    {
        RightUp = -1,
        RightDown = 1
    }

    private readonly int _direction;
    private bool _changeX;

    public DiagonalEdgeConstraint(DiagonalDirection direction)
    {
        _direction = (int)direction;
        _changeX = false;
        Label = direction switch
        {
            DiagonalDirection.RightUp => "\u21d7",
            DiagonalDirection.RightDown => "\u21d8",
            _ => throw new NotSupportedException()
        };
    }

    public EdgeType EdgeType
        => EdgeType.Line;

    public string? Label { get; }

    public IVertexContinuity DefaultContinuity
        => new G0Continuity();

    public void ApplyConstraint(Vertex a, Vertex b)
    {
        if (_changeX)
        {
            var delta = b.Y - a.Y;
            b.X = a.X + delta * _direction;
            _changeX = false;
        }
        else
        {
            var delta = b.X - a.X;
            b.Y = a.Y + delta * _direction;
            _changeX = true;
        }
    }

    public bool CheckConstraint(Vertex a, Vertex b)
        => (b.Y - a.Y).IsEqual(_direction * (b.X - a.X));

    public void ApplyBezierNeighbourConstraint(BezierCurveControlPoint oldControlPoint, BezierCurveControlPoint controlPoint,
        Vertex a, Vertex b, Vector2 tangentVector, bool shouldLengthBeEqual)
    {
        throw new NotImplementedException();
    }
}
