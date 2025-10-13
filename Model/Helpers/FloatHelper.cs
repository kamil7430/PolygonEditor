namespace PolygonEditor.Model.Helpers;

public static class FloatHelper
{
    public const float Epsilon = 1e-3f;

    public static bool IsEqual(this float a, float b)
        => Math.Abs(a - b) <= Epsilon;
}
