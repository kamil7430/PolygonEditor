namespace PolygonEditor.Model.Helpers;

public static class ConstraintSolver
{
    public static bool TryMoveVertexAndApplyConstraints(Polygon polygon, Vertex vertexMoved, PointF destination)
    {
        int movedVertexIndex = polygon.Vertices.IndexOf(vertexMoved);
        var vertices = polygon.Vertices.Clone();
        vertices[movedVertexIndex].MoveTo(destination);
        if (TryApplyConstraints(vertices, polygon.Edges, movedVertexIndex))
        {
            polygon.Vertices = vertices;
            return true;
        }
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

        return true;
    }
}
