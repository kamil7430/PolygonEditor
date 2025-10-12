namespace PolygonEditor.Model.EdgeConstraints;

public interface IEdgeConstraint
{
    string? Label { get; }
    bool CheckConstraint(Vertex a, Vertex b);
    void ApplyConstraint(Vertex a, Vertex b);
}
