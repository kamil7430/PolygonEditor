using PolygonEditor.Model.Dtos.VertexContinuities;
using PolygonEditor.Model.EdgeConstraints;
using System.Numerics;

namespace PolygonEditor.Model.VertexContinuities;

public abstract class G1C1ContinuitiesBase : IVertexContinuity
{
    protected readonly Polygon Polygon;

    public abstract string? Label { get; }

    protected G1C1ContinuitiesBase(Polygon polygon)
    {
        Polygon = polygon;
    }

    protected Vector2 GetTangentVector(Vertex vertex, Edge previousEdge)
    {
        var (v1, v2) = Polygon.GetEdgeVertices(previousEdge);
        if (v1 == vertex)
            (v1, v2) = (v2, v1);

        return previousEdge.Constraint.EdgeType switch
        {
            EdgeType.Line => GetTangentVectorForLine(v1, v2, previousEdge),
            EdgeType.Arc => GetTangentVectorForArc(v1, v2, previousEdge),
            EdgeType.BezierCurve => GetTangentVectorForBezierCurve(v1, v2, previousEdge),
            _ => throw new NotImplementedException()
        };
    }

    protected Vector2 GetTangentVectorForLine(Vertex v1, Vertex v2, Edge previousEdge)
        => (v2 - v1).ToVector2();

    protected Vector2 GetTangentVectorForArc(Vertex v1, Vertex v2, Edge previousEdge)
    {
        var center = ((CircleArcEdgeConstraint)previousEdge.Constraint).GetCircleParams(v1, v2).Center;
        var radiusVector = new Vector2(v2.X - center.X, v2.Y - center.Y);
        return new Vector2(radiusVector.Y, -radiusVector.X);
    }

    protected Vector2 GetTangentVectorForBezierCurve(Vertex v1, Vertex v2, Edge previousEdge)
    {
        var bezier = (BezierCurveEdgeConstraint)previousEdge.Constraint;
        var controlPoint = bezier.GetCorrespondingControlPoint(v2).ToVertex();
        return ((v2 - controlPoint) * 3).ToVector2();
    }

    public abstract bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type, IVertexContinuity vertex1Continuity, IVertexContinuity vertex2Continuity);

    public abstract (Vector2 TangentVector, bool ShouldLengthBeEqual)? GetContinuityConditions(Vertex vertex, Edge previousEdge, Edge currentEdge);

    public abstract object Clone();
    public abstract IVertexContinuityDto ToDto();
}
