using System.Numerics;

namespace PolygonEditor.Model.VertexContinuities;

public class C1Continuity : G1C1ContinuitiesBase
{
    public C1Continuity(Polygon polygon)
        : base(polygon)
    { }

    public override bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type, IVertexContinuity vertex1Continuity, IVertexContinuity vertex2Continuity)
        => (edge1Type == EdgeType.BezierCurve || edge2Type == EdgeType.BezierCurve)
        && (edge1Type != EdgeType.Arc && edge2Type != EdgeType.Arc);

    public override (Vector2 TangentVector, bool ShouldLengthBeEqual)? GetContinuityConditions(Vertex vertex, Edge previousEdge)
        => (GetTangentVector(vertex, previousEdge), true);

    public override object Clone()
        => new C1Continuity(Polygon);
}
