namespace PolygonEditor.Model.EdgeConstraints;

public interface IEdgeConstraint
{
    bool CheckConstraint(Vertex a, Vertex b);
    void ApplyConstraint(Vertex a, Vertex b);
}
