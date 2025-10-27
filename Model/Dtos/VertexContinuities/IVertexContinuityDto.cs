using PolygonEditor.Model.VertexContinuities;

namespace PolygonEditor.Model.Dtos.VertexContinuities;

public interface IVertexContinuityDto
{
    IVertexContinuity GetContinuity(Polygon polygon);
}
