using System.Numerics;

namespace PolygonEditor.Model.VertexContinuities;

public class G0Continuity : IVertexContinuity
{
    public bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type)
        => true;

    public (Vector2 TangentVector, bool ShouldLengthBeEqual)? GetContinuityConditions(Vertex vertex, Edge previousEdge)
        => null;

    public object Clone()
        => new G0Continuity();
}
