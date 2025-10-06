namespace PolygonEditor.View;

internal interface IPolygonEditorView
{
    event EventHandler? HelpClicked;
    event EventHandler? LibraryAlgorithmChosen;
    event EventHandler? BresenhamAlgorithmChosen;

    void ShowMessageBox(string message);
}
