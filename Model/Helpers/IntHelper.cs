namespace PolygonEditor.Model.Helpers;

public static class IntHelper
{
    public static int TrueModulo(this int a, int b)
    {
        int c = a % b;
        return (c + b) % b;
    }
}
