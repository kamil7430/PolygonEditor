namespace PolygonEditor.View;

public partial class PolygonsForm
{
    public event EventHandler? HelpClicked;
    public event EventHandler? LibraryAlgorithmChosen;
    public event EventHandler? BresenhamAlgorithmChosen;
    public event EventHandler? PolygonPanelPainting;
    public event EventHandler? PolygonPanelMouseDown;
    public event EventHandler? PolygonPanelMouseMove;
    public event EventHandler? PolygonPanelMouseUp;

    private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        => HelpClicked?.Invoke(sender, e);

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

    public event ToolStripItemClickedEventHandler? DeleteVertexClicked;
    public event ToolStripItemClickedEventHandler? ContinuityG0Clicked;
    public event ToolStripItemClickedEventHandler? ContinuityG1Clicked;
    public event ToolStripItemClickedEventHandler? ContinuityC1Clicked;
    public event ToolStripItemClickedEventHandler? AddVertexClicked;
    public event ToolStripItemClickedEventHandler? HorizontalEdgeClicked;
    public event ToolStripItemClickedEventHandler? DiagonalEdgeClicked;
    public event ToolStripItemClickedEventHandler? FixedEdgeLengthClicked;
    public event ToolStripItemClickedEventHandler? BezierCurveClicked;
    public event ToolStripItemClickedEventHandler? CircleArcClicked;
    public event ToolStripItemClickedEventHandler? RemoveConstraintClicked;
    public event ToolStripDropDownClosingEventHandler? VertexContextMenuClosing;
    public event ToolStripDropDownClosingEventHandler? EdgeContextMenuClosing;

    private void usuńWierzchołekToolStripMenuItem_Click(object sender, EventArgs e)
        => DeleteVertexClicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void ciągłośćG0ToolStripMenuItem_Click(object sender, EventArgs e)
        => ContinuityG0Clicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void ciągłośćC0ToolStripMenuItem_Click(object sender, EventArgs e)
        => ContinuityG1Clicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void ciągłośćC0ToolStripMenuItem1_Click(object sender, EventArgs e)
        => ContinuityC1Clicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void dodajWierzchołekToolStripMenuItem_Click(object sender, EventArgs e)
        => AddVertexClicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void krawędźPoziomaToolStripMenuItem_Click(object sender, EventArgs e)
        => HorizontalEdgeClicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void krawędźSkośnaToolStripMenuItem_Click(object sender, EventArgs e)
        => DiagonalEdgeClicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void stałaDługośćKrawędziToolStripMenuItem_Click(object sender, EventArgs e)
        => FixedEdgeLengthClicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void krzywaBezieraToolStripMenuItem_Click(object sender, EventArgs e)
        => BezierCurveClicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void łukOkręguToolStripMenuItem_Click(object sender, EventArgs e)
        => CircleArcClicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void usuńOgraniczenieToolStripMenuItem_Click(object sender, EventArgs e)
        => RemoveConstraintClicked?.Invoke(sender, (ToolStripItemClickedEventArgs)e);

    private void vertexContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        => VertexContextMenuClosing?.Invoke(sender, e);

    private void edgeContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        => EdgeContextMenuClosing?.Invoke(sender, e);
}
