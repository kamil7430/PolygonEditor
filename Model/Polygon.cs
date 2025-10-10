using PolygonEditor.Model.EdgeConstraints;
using PolygonEditor.Model.Helpers;
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

    public void MoveVertex(Vertex vertex, Point destination)
    {
        if (!ConstraintSolver.TryMoveVertexAndApplyConstraints(this, vertex, destination))
            MoveWholePolygon(destination.Subtract(vertex.ToPoint()).ToSize());
    }


    public void MoveWholePolygon(Size delta)
        => Vertices.ForEach(v => v.Offset(delta));

    public void DeleteVertex(Vertex vertex)
    {
        var index = Vertices.IndexOf(vertex);
        Vertices.RemoveAt(index);
        Edges.RemoveAt(index);
        index %= Edges.Count;
        Edges[index].Constraint = new NoConstraint();
    }

    public void AddVertex(Edge edge)
    {
        var index = Edges.IndexOf(edge);
        var v1 = Vertices[index];
        var v2 = Vertices[(index + 1) % Vertices.Count];
        var newVertex = new Vertex((v2.X + v1.X) / 2, (v2.Y + v1.Y) / 2);
        Vertices.Insert(index + 1, newVertex);
        Edges.Insert(index + 1, new Edge());
    }

    public Vertex? GetNearestVertexInRadius(Point point, float radius)
    {
        (Vertex Vertex, float Distance)? nearestVertex = null;
        Vector2 p = new(point.X, point.Y);
        foreach (var vertex in Vertices)
        {
            Vector2 v = new(vertex.X, vertex.Y);
            var distance = Vector2.Distance(p, v);
            if (distance <= radius)
                if (nearestVertex == null || distance < nearestVertex.Value.Distance)
                    nearestVertex = (vertex, distance);
        }
        return nearestVertex?.Vertex;
    }

    public Edge? GetNearestEdgeInRadius(Point p, double radius)
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
}
