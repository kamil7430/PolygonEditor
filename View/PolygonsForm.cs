using PolygonEditor.Presenter;

namespace PolygonEditor.View;

public partial class PolygonsForm : Form, IPolygonEditorView
{
    private const int VERTEX_RADIUS = 20;

    public PolygonsForm()
    {
        InitializeComponent();
        _ = new PolygonsFormPresenter(this);
    }

    public event EventHandler? HelpClicked;
    public event EventHandler? LibraryAlgorithmChosen;
    public event EventHandler? BresenhamAlgorithmChosen;
    public event EventHandler? PolygonPanelPainting;

    public void ShowMessageBox(string message)
        => MessageBox.Show(message);

    public void DrawLine(Graphics g, Point p1, Point p2)
        => g.DrawLine(Pens.Black, p1, p2);

    public void DrawPixels(Graphics g, IEnumerable<Point> points)
    {
        foreach (var p in points)
            g.FillRectangle(Brushes.Black, p.X, p.Y, 1, 1);
    }

    public void DrawVertex(Graphics g, Point p)
        => g.FillEllipse(Brushes.PaleVioletRed, p.X - VERTEX_RADIUS / 2,
            p.Y - VERTEX_RADIUS / 2, VERTEX_RADIUS, VERTEX_RADIUS);

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
}
