namespace PolygonEditor.Model.EdgeConstraints;

public class HorizontalEdgeConstraint : IEdgeConstraint
{
    public string? Label
        => "\u21d4";

    public void ApplyConstraint(Vertex a, Vertex b)
        => b.Y = a.Y;

    public bool CheckConstraint(Vertex a, Vertex b)
        => a.Y == b.Y;
}
