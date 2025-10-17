using System.Numerics;

namespace PolygonEditor.Model;

public struct PointD : ICastableToVector2
{
    public double X { get; set; }
    public double Y { get; set; }

    public PointD(double x, double y)
    {
        X = x;
        Y = y;
    }

    public PointF ToPointF()
        => new PointF((float)X, (float)Y);

    public Vector2 ToVector2()
        => new Vector2((float)X, (float)Y);

    public static PointD operator +(PointD a, PointD b)
        => new PointD(a.X + b.X, a.Y + b.Y);

    public static PointD operator -(PointD a, PointD b)
        => new PointD(a.X - b.X, a.Y - b.Y);

    public static PointD operator *(PointD p, double scalar)
        => new PointD(p.X * scalar, p.Y * scalar);

    public static PointD operator *(double scalar, PointD p)
        => new PointD(p.X * scalar, p.Y * scalar);
}
