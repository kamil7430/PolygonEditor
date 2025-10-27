using PolygonEditor.Model.VertexContinuities;

namespace PolygonEditor.Model.Dtos.VertexContinuities;

public class C1ContinuityDto : IVertexContinuityDto
{
    public IVertexContinuity GetContinuity(Polygon polygon)
        => new C1Continuity(polygon);
}
