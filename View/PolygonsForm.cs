using PolygonEditor.Presenter;

namespace PolygonEditor.View;

public partial class PolygonsForm : Form, IPolygonEditorView
{
    public PolygonsForm()
    {
        InitializeComponent();
        _ = new PolygonsFormPresenter(this);
    }
}
