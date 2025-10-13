namespace PolygonEditor.Model.RenderingStrategies;

public interface IRenderingStrategy
{
    bool ShouldUseLibraryDrawingFunction { get; }
    IEnumerable<PointF> GetPixelsToPaint(Polygon polygon);
}
