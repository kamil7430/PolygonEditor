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
    event ToolStripItemClickedEventHandler? DeleteVertexClicked;
    event ToolStripItemClickedEventHandler? ContinuityG0Clicked;
    event ToolStripItemClickedEventHandler? ContinuityG1Clicked;
    event ToolStripItemClickedEventHandler? ContinuityC1Clicked;
    event ToolStripItemClickedEventHandler? AddVertexClicked;
    event ToolStripItemClickedEventHandler? HorizontalEdgeClicked;
    event ToolStripItemClickedEventHandler? DiagonalEdgeClicked;
    event ToolStripItemClickedEventHandler? FixedEdgeLengthClicked;
    event ToolStripItemClickedEventHandler? BezierCurveClicked;
    event ToolStripItemClickedEventHandler? CircleArcClicked;
    event ToolStripItemClickedEventHandler? RemoveConstraintClicked;
    event ToolStripDropDownClosingEventHandler? VertexContextMenuClosing;
    event ToolStripDropDownClosingEventHandler? EdgeContextMenuClosing;

    void ShowMessageBox(string message);
    void DrawLine(Graphics g, Point p1, Point p2);
    void DrawPixels(Graphics g, IEnumerable<Point> points);
    void DrawVertex(Graphics g, Point p);
    void RefreshPolygonPanel();
    void ShowVertexContextMenu(Point location);
    void ShowEdgeContextMenu(Point location);
}
