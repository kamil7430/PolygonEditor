namespace PolygonEditor.Model.Dtos;

public class PolygonDto
{
    public List<VertexDto> Vertices { get; set; }
    public List<EdgeDto> Edges { get; set; }

    public PolygonDto(List<VertexDto> vertices, List<EdgeDto> edges)
    {
        Vertices = vertices;
        Edges = edges;
    }

    public Polygon GetPolygon()
    {
        var polygon = new Polygon([], []);
        List<Vertex> vertices = [];
        List<Edge> edges = [];
        foreach (var v in Vertices)
            vertices.Add(v.GetVertex(polygon));
        foreach (var e in Edges)
            edges.Add(e.GetEdge(polygon));
        polygon.Vertices = vertices;
        polygon.Edges = edges;
        return polygon;
    }
}
