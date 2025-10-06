namespace PolygonEditor.View;

internal interface IPolygonEditorView
{
    event EventHandler? HelpClicked;

    void ShowMessageBox(string message);
}
