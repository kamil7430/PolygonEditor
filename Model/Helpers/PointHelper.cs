using System.Numerics;

namespace PolygonEditor.Model.Helpers;

public static class PointHelper
{
    public static Point Add(this Point p1, Point p2)
        => new Point(p1.X + p2.X, p1.Y + p2.Y);

    public static Point Subtract(this Point p1, Point p2)
        => new Point(p1.X - p2.X, p1.Y - p2.Y);

    public static Size ToSize(this Point p)
        => new Size(p);

    public static Vector2 ToVector2(this Point p)
        => new Vector2(p.X, p.Y);
}
