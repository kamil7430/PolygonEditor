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
}
