using System.Numerics;

namespace PolygonEditor.Model.Helpers;

public static class DistanceCalculator
{
    public static float DistanceTo(this Point p1, Point p2)
        => Vector2.Distance(p1.ToVector2(), p2.ToVector2());

    public static float DistanceTo(this Vertex p1, Point p2)
        => Vector2.Distance(p1.ToVector2(), p2.ToVector2());

    public static float DistanceTo(this Point p1, Vertex p2)
        => Vector2.Distance(p1.ToVector2(), p2.ToVector2());

    public static float DistanceTo(this Vertex p1, Vertex p2)
        => Vector2.Distance(p1.ToVector2(), p2.ToVector2());
}
