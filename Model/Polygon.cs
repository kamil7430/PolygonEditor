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
}
