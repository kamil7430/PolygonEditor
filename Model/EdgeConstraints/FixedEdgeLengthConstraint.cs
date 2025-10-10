using PolygonEditor.Model.Helpers;

namespace PolygonEditor.Model.EdgeConstraints;

public class FixedEdgeLengthConstraint : IEdgeConstraint
{
    private readonly int _length;

    public FixedEdgeLengthConstraint(int length)
    {
        _length = length;
    }

    public void ApplyConstraint(Vertex a, Vertex b)
    {
        double realLength = Math.Round(a.DistanceTo(b));
        double scale = _length / realLength;
        b.Scale(scale);
    }

    public bool CheckConstraint(Vertex a, Vertex b)
        => (int)Math.Round(a.DistanceTo(b)) == _length;
}
