namespace PolygonEditor.View;

public interface IPolygonEditorView
{
    int VertexRadius { get; }

    event EventHandler? HelpClicked;
    event EventHandler? LibraryAlgorithmChosen;
    event EventHandler? BresenhamAlgorithmChosen;
    event EventHandler? PolygonPanelPainting;
    event EventHandler? PolygonPanelMouseDown;
    event EventHandler? PolygonPanelMouseMove;
    event EventHandler? PolygonPanelMouseUp;
    event KeyEventHandler? FormKeyDown;
    event KeyEventHandler? FormKeyUp;
    event EventHandler? DeleteVertexClicked;
    event EventHandler? ContinuityG0Clicked;
    event EventHandler? ContinuityG1Clicked;
    event EventHandler? ContinuityC1Clicked;
    event EventHandler? AddVertexClicked;
    event EventHandler? HorizontalEdgeClicked;
    event EventHandler? DiagonalEdgeClicked;
    event EventHandler? FixedEdgeLengthClicked;
    event EventHandler? BezierCurveClicked;
    event EventHandler? CircleArcClicked;
    event EventHandler? RemoveConstraintClicked;
    event ToolStripDropDownClosingEventHandler? VertexContextMenuClosing;
    event ToolStripDropDownClosingEventHandler? EdgeContextMenuClosing;

    void ShowMessageBox(string message);
    void DrawLine(Graphics g, Point p1, Point p2);
    void DrawPixels(Graphics g, IEnumerable<Point> points);
    void DrawVertex(Graphics g, Point p);
    void RefreshPolygonPanel();
    void ShowVertexContextMenu(Point location);
    void ShowEdgeContextMenu(Point location);
    int? ShowFixedEdgeLengthForm(int actualLength);
}
