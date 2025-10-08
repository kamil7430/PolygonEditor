namespace PolygonEditor.Model.RenderingStrategies;

public interface IRenderingStrategy
{
    bool ShouldUseLibraryDrawingFunction { get; }
    IEnumerable<Point> GetPixelsToPaint(Polygon polygon);
}
