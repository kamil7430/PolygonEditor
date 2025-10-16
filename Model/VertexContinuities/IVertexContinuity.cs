namespace PolygonEditor.Model.VertexContinuities;

public interface IVertexContinuity : ICloneable
{
    bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type);
}
