namespace PolygonEditor.Model.RenderingStrategies;

public class LibraryDrawingFunctionStrategy : IRenderingStrategy
{
    public bool ShouldUseLibraryDrawingFunction
        => true;

    public IEnumerable<PointF> GetPixelsToPaint(Polygon polygon, IEnumerable<Edge> edgesToPaint)
        => throw new NotImplementedException();
}
