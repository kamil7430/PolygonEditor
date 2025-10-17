using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Helpers;

public static class ConstraintSolver
{
    public static bool TryMoveVertexAndApplyConstraints(Polygon polygon, Vertex vertexMoved, PointF destination)
    {
        int movedVertexIndex = polygon.Vertices.IndexOf(vertexMoved);
        var vertices = polygon.Vertices.Clone();
        polygon.Vertices[movedVertexIndex].MoveTo(destination);

        if (TryApplyConstraints(polygon.Vertices, polygon.Edges, movedVertexIndex))
            return true;

        polygon.Vertices = vertices;
        return false;
    }

    private static bool TryApplyConstraints(List<Vertex> vertices, List<Edge> edges, int movedVertexIndex)
    {
        int vertexCount = vertices.Count;
        int i = movedVertexIndex, j = (i + 1).TrueModulo(vertexCount);
        while (!edges[i].Constraint.CheckConstraint(vertices[i], vertices[j]))
        {
            edges[i].Constraint.ApplyConstraint(vertices[i], vertices[j]);
            i = j;
            j = (j + 1).TrueModulo(vertexCount);
            if (j == movedVertexIndex)
                break;
        }
        if (j != movedVertexIndex && edges[j].Constraint is BezierCurveEdgeConstraint)
            edges[j].Constraint.ApplyConstraint(vertices[j], vertices[(j + 1).TrueModulo(vertexCount)]);

        int lastTouchedIndex = i;
        i = movedVertexIndex;
        j = (i - 1).TrueModulo(vertexCount);
        while (!edges[(i - 1).TrueModulo(vertexCount)].Constraint.CheckConstraint(vertices[i], vertices[j]))
        {
            edges[(i - 1).TrueModulo(vertexCount)].Constraint.ApplyConstraint(vertices[i], vertices[j]);
            i = j;
            j = (j - 1).TrueModulo(vertexCount);
            if (lastTouchedIndex == i)
                return false;
        }
        if (edges[(j - 1).TrueModulo(vertexCount)].Constraint is BezierCurveEdgeConstraint)
            edges[(j - 1).TrueModulo(vertexCount)].Constraint.ApplyConstraint(vertices[j], vertices[(j - 1).TrueModulo(vertexCount)]);

        return true;
    }
}
