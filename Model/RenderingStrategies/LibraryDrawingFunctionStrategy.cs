namespace PolygonEditor.Model.RenderingStrategies;

public class LibraryDrawingFunctionStrategy : IRenderingStrategy
{
    public bool ShouldUseLibraryDrawingFunction
        => true;

    public IEnumerable<PointF> GetPixelsToPaint(Polygon polygon)
        => throw new NotImplementedException();
}
