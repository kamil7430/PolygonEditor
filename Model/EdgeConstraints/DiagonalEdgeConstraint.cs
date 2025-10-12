namespace PolygonEditor.Model.EdgeConstraints;

public class DiagonalEdgeConstraint : IEdgeConstraint
{
    public enum DiagonalDirection
    {
        RightUp = -1,
        RightDown = 1
    }

    private readonly int _direction;
    private bool _changeX;

    public DiagonalEdgeConstraint(DiagonalDirection direction)
    {
        _direction = (int)direction;
        _changeX = false;
    }

    public void ApplyConstraint(Vertex a, Vertex b)
    {
        if (_changeX)
        {
            var delta = b.Y - a.Y;
            b.X = a.X + delta * _direction;
            _changeX = false;
        }
        else
        {
            var delta = b.X - a.X;
            b.Y = a.Y + delta * _direction;
            _changeX = true;
        }
    }

    public bool CheckConstraint(Vertex a, Vertex b)
        => _direction * (b.X - a.X) == b.Y - a.Y;
}
