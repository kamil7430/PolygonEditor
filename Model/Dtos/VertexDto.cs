namespace PolygonEditor.Model.Dtos;

public class VertexDto
{
    public float X { get; set; }
    public float Y { get; set; }
    public IVertexContinuityDto Continuity { get; set; }
}
