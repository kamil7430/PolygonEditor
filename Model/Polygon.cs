namespace PolygonEditor.Model;

internal class Polygon
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
            [new(10, 15), new(130, 30), new(140, 125), new(25, 30)],
            [new(), new(), new(), new()]
        );
}
