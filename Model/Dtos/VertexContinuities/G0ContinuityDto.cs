using PolygonEditor.Model.VertexContinuities;

namespace PolygonEditor.Model.Dtos.VertexContinuities;

public class G0ContinuityDto : IVertexContinuityDto
{
    public IVertexContinuity GetContinuity(Polygon polygon)
        => new G0Continuity();
}
