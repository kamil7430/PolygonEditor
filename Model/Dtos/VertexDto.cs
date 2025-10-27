using PolygonEditor.Model.Dtos.VertexContinuities;

namespace PolygonEditor.Model.Dtos;

public class VertexDto
{
    public float X { get; set; }
    public float Y { get; set; }
    public IVertexContinuityDto Continuity { get; set; }

    public VertexDto(float x, float y, IVertexContinuityDto continuity)
    {
        X = x;
        Y = y;
        Continuity = continuity;
    }
}
