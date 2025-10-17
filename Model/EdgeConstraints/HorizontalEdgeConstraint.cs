using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;

namespace PolygonEditor.Model.EdgeConstraints;

public class HorizontalEdgeConstraint : IEdgeConstraint
{
    public EdgeType EdgeType
        => EdgeType.Line;

    public string? Label
        => "\u21d4";

    public IVertexContinuity DefaultContinuity
        => new G0Continuity();

    public void ApplyConstraint(Vertex a, Vertex b)
        => b.Y = a.Y;

    public bool CheckConstraint(Vertex a, Vertex b)
        => a.Y.IsEqual(b.Y);
}
