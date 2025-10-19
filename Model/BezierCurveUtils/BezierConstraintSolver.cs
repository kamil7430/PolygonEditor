using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.BezierCurveUtils;

public static class BezierConstraintSolver
{
    public static (BezierCurveControlPoint OldControlPoint, Vertex Vertex, Vertex OldVertexToMove) MoveFirstEdge(Polygon polygon,
        BezierCurveControlPoint controlPoint, Vertex vertex, Vertex vertexToMove, BezierCurveEdgeConstraint bezierConstraint,
        Edge bezierEdge, Edge edgeToMove, PointF destination)
    {
        var oldControlPoint = (BezierCurveControlPoint)controlPoint.Clone();
        var oldVertex = (Vertex)vertex.Clone();
        var oldVertexToMove = (Vertex)vertexToMove.Clone();

        controlPoint.MoveTo(destination);
        var continuityConditions = vertex.Continuity.GetContinuityConditions(vertex, bezierEdge, edgeToMove);
        if (continuityConditions != null)
            edgeToMove.Constraint.ApplyBezierNeighbourConstraint(vertex, vertexToMove,
                continuityConditions.Value.TangentVector, continuityConditions.Value.ShouldLengthBeEqual);

        return (oldControlPoint, oldVertex, oldVertexToMove);
    }
}
