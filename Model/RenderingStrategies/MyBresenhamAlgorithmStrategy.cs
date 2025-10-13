namespace PolygonEditor.Model.RenderingStrategies;

public class MyBresenhamAlgorithmStrategy : IRenderingStrategy
{
    public bool ShouldUseLibraryDrawingFunction
        => false;

    public IEnumerable<PointF> GetPixelsToPaint(Polygon polygon)
    {
        throw new NotImplementedException();
    }
}
