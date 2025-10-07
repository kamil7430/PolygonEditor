namespace PolygonEditor.Model.RenderingStrategies;

internal class LibraryDrawingFunctionStrategy : IRenderingStrategy
{
    public bool ShouldUseLibraryDrawingFunction
        => true;

    public IEnumerable<Point> GetPixelsToPaint(Vertex v1, Vertex v2)
        => throw new NotImplementedException();
}
