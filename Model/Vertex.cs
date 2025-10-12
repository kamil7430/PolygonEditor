using System.Numerics;

namespace PolygonEditor.Model;

public class Vertex : ICastableToVector2, ICloneable
{
    public int X { get; set; }
    public int Y { get; set; }
    public ContinuityType ContinuityType { get; set; }

    public Vertex(int x, int y, ContinuityType continuityType = ContinuityType.G0)
    {
        X = x;
        Y = y;
        ContinuityType = continuityType;
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

    public static Vertex operator +(Vertex a, Vertex b)
        => new Vertex(a.X + b.X, a.Y + b.Y);

    public static Vertex operator -(Vertex a, Vertex b)
        => new Vertex(a.X - b.X, a.Y - b.Y);

    public static Vertex operator *(Vertex a, int b)
        => new Vertex(a.X * b, a.Y * b, a.ContinuityType);

    public static Vertex operator /(Vertex a, int b)
        => new Vertex(a.X / b, a.Y / b, a.ContinuityType);

    public Point ToPoint()
        => new Point(X, Y);

    public Vector2 ToVector2()
        => new Vector2(X, Y);

    public object Clone()
        => new Vertex(X, Y, ContinuityType);
}
