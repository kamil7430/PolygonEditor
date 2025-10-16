namespace PolygonEditor.Model.VertexContinuities;

public class G1Continuity : IVertexContinuity
{
    public bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type)
        => edge1Type != EdgeType.Line || edge2Type != EdgeType.Line;

    public object Clone()
        => new G1Continuity();
}
