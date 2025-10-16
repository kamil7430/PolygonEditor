namespace PolygonEditor.Model.VertexContinuities;

public class C1Continuity : IVertexContinuity
{
    public bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type)
        => edge1Type == EdgeType.BezierCurve || edge2Type == EdgeType.BezierCurve;

    public object Clone()
        => new C1Continuity();
}
