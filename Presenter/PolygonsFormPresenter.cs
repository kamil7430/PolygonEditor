using PolygonEditor.Model.RenderingStrategies;
using PolygonEditor.View;

namespace PolygonEditor.Presenter;

internal class PolygonsFormPresenter
{
    private readonly IPolygonEditorView _view;
    private IRenderingStrategy _renderingStrategy;

    public PolygonsFormPresenter(IPolygonEditorView view)
    {
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

    private void SubscribeToEvents()
    {
        _view.HelpClicked += HelpClicked;
        _view.LibraryAlgorithmChosen += LibraryAlgorithmChosen;
        _view.BresenhamAlgorithmChosen += BresenhamAlgorithmChosen;
    }
}
