using PolygonEditor.Model;

namespace PolygonEditor.View;

internal interface IPolygonEditorView
{
    event EventHandler? HelpClicked;
    event EventHandler? LibraryAlgorithmChosen;
    event EventHandler? BresenhamAlgorithmChosen;

    void ShowMessageBox(string message);
    void DrawLine(Vertex v1, Vertex v2);
}
