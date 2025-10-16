namespace PolygonEditor.Model.VertexContinuities;

public class G0Continuity : IVertexContinuity
{
    public bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type)
        => true;

    public object Clone()
        => new G0Continuity();
}
