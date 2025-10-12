namespace PolygonEditor.Model.EdgeConstraints;

public class NoConstraint : IEdgeConstraint
{
    public EdgeType EdgeType
        => EdgeType.Line;

    public string? Label
        => "";

    public void ApplyConstraint(Vertex a, Vertex b)
    { }

    public bool CheckConstraint(Vertex a, Vertex b)
        => true;
}
