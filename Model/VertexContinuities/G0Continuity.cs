using System.Numerics;

namespace PolygonEditor.Model.VertexContinuities;

public class G0Continuity : IVertexContinuity
{
    public bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type, IVertexContinuity vertex1Continuity, IVertexContinuity vertex2Continuity)
        => true;

    public (Vector2 TangentVector, bool ShouldLengthBeEqual)? GetContinuityConditions(Vertex vertex, Edge previousEdge, Edge currentEdge)
        => null;

    public object Clone()
        => new G0Continuity();
}
