using System.Numerics;

namespace PolygonEditor.Model;

public class Vertex : ICastableToVector2
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vertex(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point ToPoint()
        => new Point(X, Y);

    public Vector2 ToVector2()
        => new Vector2(X, Y);

    public void Offset(Size size)
    {
        X += size.Width;
        Y += size.Height;
    }
}
