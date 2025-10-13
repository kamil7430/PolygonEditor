namespace PolygonEditor.View;

public partial class PolygonsForm
{
    public event EventHandler? HelpClicked;
    public event EventHandler? PolygonResetClicked;
    public event EventHandler? LibraryAlgorithmChosen;
    public event EventHandler? BresenhamAlgorithmChosen;
    public event EventHandler? PolygonPanelPainting;
    public event EventHandler? PolygonPanelMouseDown;
    public event EventHandler? PolygonPanelMouseMove;
    public event EventHandler? PolygonPanelMouseUp;
    public event KeyEventHandler? FormKeyDown;
    public event KeyEventHandler? FormKeyUp;

    private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        => HelpClicked?.Invoke(sender, e);

    private void resetWielokątaToolStripMenuItem_Click(object sender, EventArgs e)
        => PolygonResetClicked?.Invoke(sender, e);

    private void bresenhamAlgorithmRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton radioButton && radioButton.Checked)
            BresenhamAlgorithmChosen?.Invoke(sender, e);
    }

    private void libraryAlgorithmRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton radioButton && radioButton.Checked)
            LibraryAlgorithmChosen?.Invoke(sender, e);
    }

    private void polygonPanel_Paint(object sender, PaintEventArgs e)
        => PolygonPanelPainting?.Invoke(sender, e);

    private void polygonPanel_MouseDown(object sender, MouseEventArgs e)
        => PolygonPanelMouseDown?.Invoke(sender, e);

    private void polygonPanel_MouseMove(object sender, MouseEventArgs e)
        => PolygonPanelMouseMove?.Invoke(sender, e);

    private void polygonPanel_MouseUp(object sender, MouseEventArgs e)
        => PolygonPanelMouseUp?.Invoke(sender, e);

    private void PolygonsForm_KeyDown(object sender, KeyEventArgs e)
        => FormKeyDown?.Invoke(sender, e);

    private void PolygonsForm_KeyUp(object sender, KeyEventArgs e)
        => FormKeyUp?.Invoke(sender, e);

    public event EventHandler? DeleteVertexClicked;
    public event EventHandler? ContinuityG0Clicked;
    public event EventHandler? ContinuityG1Clicked;
    public event EventHandler? ContinuityC1Clicked;
    public event EventHandler? AddVertexClicked;
    public event EventHandler? HorizontalEdgeClicked;
    public event EventHandler? DiagonalUpEdgeClicked;
    public event EventHandler? DiagonalDownEdgeClicked;
    public event EventHandler? FixedEdgeLengthClicked;
    public event EventHandler? BezierCurveClicked;
    public event EventHandler? CircleArcClicked;
    public event EventHandler? RemoveConstraintClicked;
    public event ToolStripDropDownClosingEventHandler? VertexContextMenuClosing;
    public event ToolStripDropDownClosingEventHandler? EdgeContextMenuClosing;

    private void usuńWierzchołekToolStripMenuItem_Click(object sender, EventArgs e)
        => DeleteVertexClicked?.Invoke(sender, e);

    private void ciągłośćG0ToolStripMenuItem_Click(object sender, EventArgs e)
        => ContinuityG0Clicked?.Invoke(sender, e);

    private void ciągłośćC0ToolStripMenuItem_Click(object sender, EventArgs e)
        => ContinuityG1Clicked?.Invoke(sender, e);

    private void ciągłośćC0ToolStripMenuItem1_Click(object sender, EventArgs e)
        => ContinuityC1Clicked?.Invoke(sender, e);

    private void dodajWierzchołekToolStripMenuItem_Click(object sender, EventArgs e)
        => AddVertexClicked?.Invoke(sender, e);

    private void krawędźPoziomaToolStripMenuItem_Click(object sender, EventArgs e)
        => HorizontalEdgeClicked?.Invoke(sender, e);

    private void krawędźSkośnaToolStripMenuItem_Click(object sender, EventArgs e)
        => DiagonalUpEdgeClicked?.Invoke(sender, e);

    private void diagonalDownToolStripMenuItem_Click(object sender, EventArgs e)
        => DiagonalDownEdgeClicked?.Invoke(sender, e);

    private void stałaDługośćKrawędziToolStripMenuItem_Click(object sender, EventArgs e)
        => FixedEdgeLengthClicked?.Invoke(sender, e);

    private void krzywaBezieraToolStripMenuItem_Click(object sender, EventArgs e)
        => BezierCurveClicked?.Invoke(sender, e);

    private void łukOkręguToolStripMenuItem_Click(object sender, EventArgs e)
        => CircleArcClicked?.Invoke(sender, e);

    private void usuńOgraniczenieToolStripMenuItem_Click(object sender, EventArgs e)
        => RemoveConstraintClicked?.Invoke(sender, e);

    private void vertexContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        => VertexContextMenuClosing?.Invoke(sender, e);

    private void edgeContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        => EdgeContextMenuClosing?.Invoke(sender, e);
}
