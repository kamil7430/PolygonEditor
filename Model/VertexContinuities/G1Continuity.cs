using System.Numerics;

namespace PolygonEditor.Model.VertexContinuities;

public class G1Continuity : G1C1ContinuitiesBase
{
    public G1Continuity(Polygon polygon)
        : base(polygon)
    { }

    public override bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type)
        => edge1Type != EdgeType.Line || edge2Type != EdgeType.Line;

    public override (Vector2 TangentVector, bool ShouldLengthBeEqual)? GetContinuityConditions(Vertex vertex, Edge previousEdge)
        => (GetTangentVector(vertex, previousEdge), false);

    public override object Clone()
        => new G1Continuity(Polygon);
}
