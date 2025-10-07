namespace PolygonEditor.Model.RenderingStrategies;

internal class MyBresenhamAlgorithmStrategy : IRenderingStrategy
{
    public bool ShouldUseLibraryDrawingFunction
        => false;

    public IEnumerable<Point> GetPixelsToPaint(Vertex v1, Vertex v2)
    {
        throw new NotImplementedException();
    }
}
