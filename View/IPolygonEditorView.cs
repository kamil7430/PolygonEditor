namespace PolygonEditor.View;

public interface IPolygonEditorView
{
    event EventHandler? HelpClicked;
    event EventHandler? LibraryAlgorithmChosen;
    event EventHandler? BresenhamAlgorithmChosen;
    event EventHandler? PolygonPanelPainting;
    event EventHandler? PolygonPanelMouseDown;
    event EventHandler? PolygonPanelMouseUp;

    void ShowMessageBox(string message);
    void DrawLine(Graphics g, Point p1, Point p2);
    void DrawPixels(Graphics g, IEnumerable<Point> points);
    void DrawVertex(Graphics g, Point p);
}
