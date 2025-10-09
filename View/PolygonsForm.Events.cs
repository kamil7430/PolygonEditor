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
}
