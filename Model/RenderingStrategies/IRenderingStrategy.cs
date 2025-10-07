namespace PolygonEditor.Model.RenderingStrategies;

internal interface IRenderingStrategy
{
    bool ShouldUseLibraryDrawingFunction { get; }
    IEnumerable<Point> GetPixelsToPaint(Vertex v1, Vertex v2);
}
