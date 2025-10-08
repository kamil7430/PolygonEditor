using PolygonEditor.Model;
using PolygonEditor.Model.RenderingStrategies;
using PolygonEditor.View;

namespace PolygonEditor.Presenter;

public class PolygonsFormPresenter
{
    private Polygon _polygon;

    private readonly IPolygonEditorView _view;
    private IRenderingStrategy _renderingStrategy;

    public PolygonsFormPresenter(IPolygonEditorView view)
    {
        _polygon = Polygon.Predefined;
        _view = view;
        _renderingStrategy = new LibraryDrawingFunctionStrategy();
        SubscribeToEvents();
    }

    private void HelpClicked(object? sender, EventArgs e)
        => _view.ShowMessageBox("To jest pomoc.");

    private void LibraryAlgorithmChosen(object? sender, EventArgs e)
        => _renderingStrategy = new LibraryDrawingFunctionStrategy();

    private void BresenhamAlgorithmChosen(object? sender, EventArgs e)
        => _renderingStrategy = new MyBresenhamAlgorithmStrategy();

    private void PolygonPanelPainting(object? sender, EventArgs e)
    {
        if (e is PaintEventArgs paintEventArgs)
        {
            var pixels = DrawLines(paintEventArgs.Graphics);
            if (pixels != null)
                _view.DrawPixels(paintEventArgs.Graphics, pixels);
            DrawVertices(paintEventArgs.Graphics);
        }
    }

    private IEnumerable<Point>? DrawLines(Graphics g)
    {
        if (!_renderingStrategy.ShouldUseLibraryDrawingFunction)
            return _renderingStrategy.GetPixelsToPaint(_polygon);
        var vertices = _polygon.Vertices;
        int vertexCount = vertices.Count;
        for (int i = 0; i < vertexCount; i++)
            _view.DrawLine(g, vertices[i].ToPoint(), vertices[(i + 1) % vertexCount].ToPoint());
        return null;
    }

    private void DrawVertices(Graphics g)
    {
        foreach (var vertex in _polygon.Vertices)
            _view.DrawVertex(g, vertex.ToPoint());
    }

    private void SubscribeToEvents()
    {
        _view.HelpClicked += HelpClicked;
        _view.LibraryAlgorithmChosen += LibraryAlgorithmChosen;
        _view.BresenhamAlgorithmChosen += BresenhamAlgorithmChosen;
        _view.PolygonPanelPainting += PolygonPanelPainting;
    }
}
