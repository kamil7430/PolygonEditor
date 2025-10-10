using System.Numerics;

namespace PolygonEditor.Model;

public class Vertex : ICastableToVector2, ICloneable
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vertex(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void MoveTo(Point point)
    {
        X = point.X;
        Y = point.Y;
    }

    public void Offset(Size size)
    {
        X += size.Width;
        Y += size.Height;
    }

    public void Scale(double scale)
    {
        X = (int)Math.Round(scale * X);
        Y = (int)Math.Round(scale * Y);
    }

    public Point ToPoint()
        => new Point(X, Y);

    public Vector2 ToVector2()
        => new Vector2(X, Y);

    public object Clone()
        => new Vertex(X, Y);
}
