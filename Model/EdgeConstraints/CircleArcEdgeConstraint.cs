namespace PolygonEditor.Model.EdgeConstraints;

public class CircleArcEdgeConstraint : IEdgeConstraint
{
    public EdgeType EdgeType
        => EdgeType.Arc;

    public string? Label => throw new NotImplementedException();

    public void ApplyConstraint(Vertex a, Vertex b)
    {
        throw new NotImplementedException();
    }

    public bool CheckConstraint(Vertex a, Vertex b)
    {
        throw new NotImplementedException();
    }
}
