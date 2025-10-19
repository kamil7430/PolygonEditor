using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;
using System.Numerics;

namespace PolygonEditor.Model.EdgeConstraints;

public class FixedEdgeLengthConstraint : IEdgeConstraint
{
    private readonly float _length;

    public FixedEdgeLengthConstraint(float length)
    {
        _length = length;
        Label = length.ToString("F1");
    }

    public EdgeType EdgeType
        => EdgeType.Line;

    public string? Label { get; }

    public IVertexContinuity DefaultContinuity
        => new G0Continuity();

    public void ApplyConstraint(Vertex a, Vertex b)
    {
        float realLength = a.DistanceTo(b);
        float scale = _length / realLength;
        var delta = b.ToPointF().Subtract(a.ToPointF()).ToSizeF();
        delta.Width *= scale - 1;
        delta.Height *= scale - 1;
        b.Offset(delta);
    }

    public bool CheckConstraint(Vertex a, Vertex b)
        => a.DistanceTo(b).IsEqual(_length);

    public void ApplyBezierNeighbourConstraint(BezierCurveControlPoint oldControlPoint, BezierCurveControlPoint controlPoint,
        Vertex a, Vertex b, Vector2 tangentVector, bool shouldLengthBeEqual)
    {
        if (shouldLengthBeEqual)
        {
            var delta = new SizeF(controlPoint.X - oldControlPoint.X, controlPoint.Y - oldControlPoint.Y);
            a.Offset(delta);
            b.Offset(delta);
        }
        else
            VertexHelper.ParallelToVectorKeepingLength(a, b, tangentVector);
    }
}
