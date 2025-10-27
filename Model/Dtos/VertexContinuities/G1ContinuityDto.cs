using PolygonEditor.Model.VertexContinuities;

namespace PolygonEditor.Model.Dtos.VertexContinuities;

public class G1ContinuityDto : IVertexContinuityDto
{
    public IVertexContinuity GetContinuity(Polygon polygon)
        => new G1Continuity(polygon);
}
