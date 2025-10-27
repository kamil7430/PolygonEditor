using PolygonEditor.Model.VertexContinuities;
using System.Text.Json.Serialization;

namespace PolygonEditor.Model.Dtos.VertexContinuities;

[JsonDerivedType(typeof(G0ContinuityDto), 0)]
[JsonDerivedType(typeof(G1ContinuityDto), 1)]
[JsonDerivedType(typeof(C1ContinuityDto), 2)]
public interface IVertexContinuityDto
{
    IVertexContinuity GetContinuity(Polygon polygon);
}
