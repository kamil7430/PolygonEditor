using System.Numerics;

namespace PolygonEditor.Model.VertexContinuities;

public interface IVertexContinuity : ICloneable
{
    string? Label { get; }
    bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type, IVertexContinuity vertex1Continuity, IVertexContinuity vertex2Continuity);
    (Vector2 TangentVector, bool ShouldLengthBeEqual)? GetContinuityConditions(Vertex vertex, Edge previousEdge, Edge currentEdge);
}
