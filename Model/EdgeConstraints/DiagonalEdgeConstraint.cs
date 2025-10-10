namespace PolygonEditor.Model.EdgeConstraints;

public class DiagonalEdgeConstraint : IEdgeConstraint
{
    public void ApplyConstraint(Vertex a, Vertex b)
    {
        throw new NotImplementedException();
    }

    public bool CheckConstraint(Vertex a, Vertex b)
        => Math.Abs(b.X - a.X) == Math.Abs(b.Y - a.Y);
}
