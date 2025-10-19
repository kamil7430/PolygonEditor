using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;
using System.Numerics;

namespace PolygonEditor.Model.EdgeConstraints;

public class NoConstraint : IEdgeConstraint
{
    public EdgeType EdgeType
        => EdgeType.Line;

    public string? Label
        => "";

    public IVertexContinuity DefaultContinuity
        => new G0Continuity();

    public void ApplyConstraint(Vertex a, Vertex b)
    { }

    public bool CheckConstraint(Vertex a, Vertex b)
        => true;

    public void ApplyBezierNeighbourConstraint(BezierCurveControlPoint oldControlPoint, BezierCurveControlPoint controlPoint,
        Vertex a, Vertex b, Vector2 tangentVector, bool shouldLengthBeEqual)
    {
        if (shouldLengthBeEqual)
        {
            b.X = a.X + tangentVector.X;
            b.Y = a.Y + tangentVector.Y;
        }
        else
            VertexHelper.ParallelToVectorKeepingLength(a, b, tangentVector);
    }
}
