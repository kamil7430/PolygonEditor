namespace PolygonEditor;

public static class PointHelper
{
    public static Point Add(this Point p1, Point p2)
        => new Point(p1.X + p2.X, p1.Y + p2.Y);

    public static Point Subtract(this Point p1, Point p2)
        => new Point(p1.X - p2.X, p1.Y - p2.Y);

    public static Size ToSize(this Point p)
        => new Size(p);
}
