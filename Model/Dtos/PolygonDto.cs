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
}
