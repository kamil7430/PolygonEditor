using PolygonEditor.View;

namespace PolygonEditor.Presenter;

internal class PolygonsFormPresenter
{
    private readonly IPolygonEditorView _view;
    private RenderingAlgorithm _renderingAlgorithm;

    public PolygonsFormPresenter(IPolygonEditorView view)
    {
        _view = view;
        _renderingAlgorithm = RenderingAlgorithm.LibraryAlgorithm;
        SubscribeToEvents();
    }

    private void HelpClicked(object? sender, EventArgs e)
        => _view.ShowMessageBox("To jest pomoc.");

    private void LibraryAlgorithmChosen(object? sender, EventArgs e)
        => _renderingAlgorithm = RenderingAlgorithm.LibraryAlgorithm;

    private void BresenhamAlgorithmChosen(object? sender, EventArgs e)
        => _renderingAlgorithm = RenderingAlgorithm.MyBresenhamAlgorithm;

    private void SubscribeToEvents()
    {
        _view.HelpClicked += HelpClicked;
        _view.LibraryAlgorithmChosen += LibraryAlgorithmChosen;
        _view.BresenhamAlgorithmChosen += BresenhamAlgorithmChosen;
    }
}
