namespace PolygonEditor.Model.Helpers;

public static class EnumerableHelper
{
    public static List<T> Clone<T>(this List<T> toClone)
        where T : ICloneable
    {
        List<T> cloned = new(toClone.Count);
        foreach (var item in toClone)
            cloned.Add((T)item.Clone());
        return cloned;
    }
}
