using PolygonEditor.Presenter;

namespace PolygonEditor.View;

public partial class PolygonsForm : Form, IPolygonEditorView
{
    public PolygonsForm()
    {
        InitializeComponent();
        _ = new PolygonsFormPresenter(this);
    }

    public event EventHandler? HelpClicked;

    public void ShowMessageBox(string message)
        => MessageBox.Show(message);

    private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        => HelpClicked?.Invoke(sender, e);
}
