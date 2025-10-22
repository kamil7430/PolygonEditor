namespace PolygonEditor.View;

public interface IPolygonEditorView
{
    // Interfejs zawierający deklaracje wszystkich eventów i metod, jakie
    // implementuje konkretny widok (w tym przypadku jeden - PolygonsForm).

    float VertexRadius { get; }
    string KeyBindingsDescription { get; }

    event EventHandler? HelpClicked;
    event EventHandler? PolygonResetClicked;
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
    event EventHandler? DiagonalUpEdgeClicked;
    event EventHandler? DiagonalDownEdgeClicked;
    event EventHandler? FixedEdgeLengthClicked;
    event EventHandler? BezierCurveClicked;
    event EventHandler? CircleArcClicked;
    event EventHandler? RemoveConstraintClicked;
    event ToolStripDropDownClosingEventHandler? VertexContextMenuClosing;
    event ToolStripDropDownClosingEventHandler? EdgeContextMenuClosing;

    void ShowMessageBox(string message);
    void DrawLine(Graphics g, PointF p1, PointF p2);
    void DrawDashedLine(Graphics g, PointF p1, PointF p2);
    void DrawArc(Graphics g, PointF center, float radius, float startAngle, float sweepAngle);
    void DrawPixels(Graphics g, IEnumerable<PointF> points);
    void DrawVertex(Graphics g, PointF p, string? label);
    void DrawControlPoint(Graphics g, PointF p);
    void DrawString(Graphics g, string text, PointF point);
    void RefreshPolygonPanel();
    void ShowVertexContextMenu(PointF location);
    void ShowEdgeContextMenu(PointF location);
    float? ShowFixedEdgeLengthForm(float actualLength);
}
