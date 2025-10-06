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
    public event EventHandler? LibraryAlgorithmChosen;
    public event EventHandler? BresenhamAlgorithmChosen;

    public void ShowMessageBox(string message)
        => MessageBox.Show(message);

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
}
