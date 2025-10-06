using PolygonEditor.View;

namespace PolygonEditor.Presenter;

internal class PolygonsFormPresenter
{
    private readonly IPolygonEditorView _view;

    public PolygonsFormPresenter(IPolygonEditorView view)
    {
        _view = view;
        _view.HelpClicked += HelpClicked;
    }

    private void HelpClicked(object? sender, EventArgs e)
        => _view.ShowMessageBox("To jest pomoc.");
}
