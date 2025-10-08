namespace PolygonEditor.Model.RenderingStrategies;

public class LibraryDrawingFunctionStrategy : IRenderingStrategy
{
    public bool ShouldUseLibraryDrawingFunction
        => true;

    public IEnumerable<Point> GetPixelsToPaint(Polygon polygon)
        => throw new NotImplementedException();
}
