using PolygonEditor.Model.Helpers;

namespace PolygonEditor.Model.EdgeConstraints;

public class FixedEdgeLengthConstraint : IEdgeConstraint
{
    private readonly int _length;

    public FixedEdgeLengthConstraint(int length)
    {
        _length = length;
        Label = length.ToString();
    }

    public string? Label { get; }

    public void ApplyConstraint(Vertex a, Vertex b)
    {
        double realLength = Math.Round(a.DistanceTo(b));
        double scale = _length / realLength;
        var delta = b.ToPoint().Subtract(a.ToPoint()).ToSize();
        delta.Width = (int)Math.Round(delta.Width * (scale - 1));
        delta.Height = (int)Math.Round(delta.Height * (scale - 1));
        b.Offset(delta);
    }

    public bool CheckConstraint(Vertex a, Vertex b)
        => (int)Math.Round(a.DistanceTo(b)) == _length;
}
