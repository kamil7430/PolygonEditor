using System.Numerics;

namespace PolygonEditor.Model.Helpers;

public static class PointHelper
{
    public static PointF Add(this PointF p1, PointF p2)
        => new PointF(p1.X + p2.X, p1.Y + p2.Y);

    public static PointF Subtract(this PointF p1, PointF p2)
        => new PointF(p1.X - p2.X, p1.Y - p2.Y);

    public static SizeF ToSizeF(this PointF p)
        => new SizeF(p);

    public static Vector2 ToVector2(this PointF p)
        => new Vector2(p.X, p.Y);
}
