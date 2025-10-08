namespace PolygonEditor.Model.RenderingStrategies;

public class MyBresenhamAlgorithmStrategy : IRenderingStrategy
{
    public bool ShouldUseLibraryDrawingFunction
        => false;

    public IEnumerable<Point> GetPixelsToPaint(Polygon polygon)
    {
        throw new NotImplementedException();
    }
}
