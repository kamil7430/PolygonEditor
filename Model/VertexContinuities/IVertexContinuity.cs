using System.Numerics;

namespace PolygonEditor.Model.VertexContinuities;

public interface IVertexContinuity : ICloneable
{
    bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type);
    (Vector2 TangentVector, bool ShouldLengthBeEqual)? GetContinuityConditions(Vertex vertex, Edge previousEdge);
}
