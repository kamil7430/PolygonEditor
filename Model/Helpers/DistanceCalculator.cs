using System.Numerics;

namespace PolygonEditor.Model.Helpers;

public static class DistanceCalculator
{
    public static float DistanceTo(this PointF p1, PointF p2)
        => Vector2.Distance(p1.ToVector2(), p2.ToVector2());

    public static float DistanceTo<T>(this T p1, PointF p2)
        where T : ICastableToVector2
        => Vector2.Distance(p1.ToVector2(), p2.ToVector2());

    public static float DistanceTo<T>(this PointF p1, T p2)
        where T : ICastableToVector2
        => Vector2.Distance(p1.ToVector2(), p2.ToVector2());

    public static float DistanceTo<T, U>(this T p1, U p2)
        where T : ICastableToVector2
        where U : ICastableToVector2
        => Vector2.Distance(p1.ToVector2(), p2.ToVector2());
}
