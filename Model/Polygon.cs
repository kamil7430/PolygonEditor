using PolygonEditor.Model.EdgeConstraints;
using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;
using System.Numerics;

namespace PolygonEditor.Model;

public class Polygon
{
    public List<Vertex> Vertices { get; set; }
    public List<Edge> Edges { get; set; }

    public Polygon(List<Vertex> vertices, List<Edge> edges)
    {
        if (vertices.Count != edges.Count)
            throw new ArgumentException("Vertex count does not match edge count!");
        Vertices = vertices;
        Edges = edges;
    }

    public static Polygon Predefined
        => new Polygon
        (
            [new(10, 15), new(530, 130), new(440, 425), new(75, 330)],
            [new(), new(), new(), new()]
        );

    public Vertex MoveVertex(Vertex vertex, PointF destination)
    {
        int vertexIndex = Vertices.IndexOf(vertex);
        if (!ConstraintSolver.TryMoveVertexAndApplyConstraints(this, vertex, destination))
            MoveWholePolygon(destination.Subtract(vertex.ToPointF()).ToSizeF());
        return Vertices[vertexIndex];
    }

    public void MoveWholePolygon(SizeF delta)
        => Vertices.ForEach(v => v.Offset(delta));

    public bool TryApplyConstraints(Edge edge, IEdgeConstraint constraint)
    {
        var oldConstraint = edge.Constraint;
        edge.Constraint = constraint;
        var index = Edges.IndexOf(edge);

        if (constraint is HorizontalEdgeConstraint &&
            (Edges[(index - 1).TrueModulo(Edges.Count)].Constraint is HorizontalEdgeConstraint
            || Edges[(index + 1).TrueModulo(Edges.Count)].Constraint is HorizontalEdgeConstraint))
        {
            edge.Constraint = oldConstraint;
            return false;
        }

        if (!ConstraintSolver.TryMoveVertexAndApplyConstraints(this, Vertices[index], Vertices[index].ToPointF()))
        {
            edge.Constraint = oldConstraint;
            return false;
        }

        return true;
    }

    public bool TryApplyVertexContinuity(Vertex vertex, IVertexContinuity continuity)
    {
        var (e1, e2) = GetVertexEdges(vertex);
        int index = Vertices.IndexOf(vertex);
        var v1 = Vertices[(index - 1).TrueModulo(Vertices.Count)];
        var v2 = Vertices[(index + 1).TrueModulo(Vertices.Count)];
        if (!continuity.DoesAccept(e1.Constraint.EdgeType, e2.Constraint.EdgeType, v1.Continuity, v2.Continuity))
            return false;

        var oldContinuity = vertex.Continuity;
        vertex.Continuity = continuity;
        if (!ConstraintSolver.TryMoveVertexAndApplyConstraints(this, vertex, vertex.ToPointF()))
        {
            vertex.Continuity = oldContinuity;
            return false;
        }

        return true;
    }

    public void DeleteVertex(Vertex vertex)
    {
        var index = Vertices.IndexOf(vertex);
        Vertices.RemoveAt(index);
        Edges.RemoveAt(index);
        index %= Edges.Count;
        Edges[(index - 1).TrueModulo(Edges.Count)].Constraint = new NoConstraint();
        Edges[index].Constraint = new NoConstraint();
        Vertices[(index - 1).TrueModulo(Edges.Count)].Continuity = new G0Continuity();
        Vertices[index].Continuity = new G0Continuity();
    }

    public void AddVertex(Edge edge)
    {
        var index = Edges.IndexOf(edge);
        var v1 = Vertices[index];
        var v2 = Vertices[(index + 1) % Vertices.Count];
        var newVertex = (v1 + v2) / 2;
        edge.Constraint = new NoConstraint();
        v1.Continuity = new G0Continuity();
        v2.Continuity = new G0Continuity();
        Vertices.Insert(index + 1, newVertex);
        Edges.Insert(index + 1, new Edge());
    }

    public Vertex? GetNearestVertexInRadius(PointF point, float radius)
    {
        (Vertex Vertex, float Distance)? nearestVertex = null;
        Vector2 p = point.ToVector2();
        foreach (var vertex in Vertices)
        {
            Vector2 v = vertex.ToVector2();
            var distance = Vector2.Distance(p, v);
            if (distance <= radius)
                if (nearestVertex == null || distance < nearestVertex.Value.Distance)
                    nearestVertex = (vertex, distance);
        }
        return nearestVertex?.Vertex;
    }

    public Edge? GetNearestEdgeInRadius(PointF p, float radius)
    {
        (Edge Edge, double Distance)? nearestEdge = null;
        var verticesCount = Vertices.Count;
        for (int i = 0; i < verticesCount; i++)
        {
            Vertex a = Vertices[i];
            Vertex b = Vertices[(i + 1) % verticesCount];
            var numerator = Math.Abs((b.X - a.X) * (a.Y - p.Y) - (a.X - p.X) * (b.Y - a.Y));
            var denominator = Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
            var distance = numerator / denominator;
            if (distance <= radius)
                if (nearestEdge == null || distance < nearestEdge.Value.Distance)
                    nearestEdge = (Edges[i], distance);
        }
        return nearestEdge?.Edge;
    }

    public Edge GetEdgeBetween(Vertex v1, Vertex v2)
    {
        int index1 = Vertices.IndexOf(v1);
        int index2 = Vertices.IndexOf(v2);
        if ((Math.Abs(index2 - index1).TrueModulo(Vertices.Count)) != 1)
            throw new ArgumentException("These vertices are not neighbours!");
        return Edges[index1];
    }

    public (Vertex V1, Vertex V2) GetEdgeVertices(Edge edge)
    {
        var index = Edges.IndexOf(edge);
        return (Vertices[index], Vertices[(index + 1) % Vertices.Count]);
    }

    public (Edge E1, Edge E2) GetVertexEdges(Vertex vertex)
    {
        var index = Vertices.IndexOf(vertex);
        return (Edges[(index - 1).TrueModulo(Vertices.Count)], Edges[index]);
    }

    public Edge GetOtherEdge(Vertex vertex, Edge edge)
    {
        var (e1, e2) = GetVertexEdges(vertex);
        return edge == e1 ? e2 : e1;
    }

    public float GetEdgeLength(Edge edge)
    {
        int index = Edges.IndexOf(edge);
        int nextIndex = (index + 1).TrueModulo(Edges.Count);
        return Vertices[index].DistanceTo(Vertices[nextIndex]);
    }
}
