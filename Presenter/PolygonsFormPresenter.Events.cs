using PolygonEditor.Model;
using PolygonEditor.Model.EdgeConstraints;
using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.RenderingStrategies;
using PolygonEditor.Model.VertexContinuities;

namespace PolygonEditor.Presenter;

public partial class PolygonsFormPresenter
{
    // Część implementacji klasy prezentera, zawierająca metody obsługujące
    // zdarzenia zachodzące w widoku.

    private void HelpClicked(object? sender, EventArgs e)
        => _view.ShowMessageBox(_view.KeyBindingsDescription);

    private void PolygonResetClicked(object? sender, EventArgs e)
    {
        _polygon = Polygon.Predefined;
        _vertexBeingDragged = null;
        _controlPointBeingDragged = null;
        _shouldDragWholePolygon = false;
        _sourceOfPolygonMovement = null;
        _contextMenusVertex = null;
        _contextMenusEdge = null;
        _view.RefreshPolygonPanel();
    }

    private void LibraryAlgorithmChosen(object? sender, EventArgs e)
    {
        _renderingStrategy = new LibraryDrawingFunctionStrategy();
        _view.RefreshPolygonPanel();
    }

    private void BresenhamAlgorithmChosen(object? sender, EventArgs e)
    {
        _renderingStrategy = new MyBresenhamAlgorithmStrategy();
        _view.RefreshPolygonPanel();
    }

    private void PolygonPanelPainting(object? sender, EventArgs e)
    {
        if (e is PaintEventArgs paintEventArgs)
        {
            List<Edge> lines = [];
            List<Edge> arcs = [];
            List<Edge> bezierCurves = [];

            foreach (var edge in _polygon.Edges)
                switch (edge.Constraint.EdgeType)
                {
                    case EdgeType.Line:
                        lines.Add(edge);
                        break;
                    case EdgeType.Arc:
                        arcs.Add(edge);
                        break;
                    case EdgeType.BezierCurve:
                        bezierCurves.Add(edge);
                        break;
                }

            var pixels = DrawLines(paintEventArgs.Graphics, lines);
            if (pixels != null)
                _view.DrawPixels(paintEventArgs.Graphics, pixels);
            DrawArcs(paintEventArgs.Graphics, arcs);
            DrawBezierCurves(paintEventArgs.Graphics, bezierCurves);
            DrawVertices(paintEventArgs.Graphics);
            DrawStrings(paintEventArgs.Graphics);
        }
    }

    private void PolygonPanelMouseUp(object? sender, EventArgs e)
    {
        var mouseEventArgs = (MouseEventArgs)e;
        switch (mouseEventArgs.Button)
        {
            case MouseButtons.Left:
                HandleLeftButtonUp(mouseEventArgs);
                break;
            case MouseButtons.Right:
                HandleRightButtonUp(mouseEventArgs);
                break;
        }
    }

    private void PolygonPanelMouseMove(object? sender, EventArgs e)
    {
        var mouseEventArgs = (MouseEventArgs)e;
        if (mouseEventArgs.Button == MouseButtons.Left)
        {
            if (_vertexBeingDragged != null)
            {
                _vertexBeingDragged = _polygon.MoveVertex(_vertexBeingDragged, mouseEventArgs.Location);
                _view.RefreshPolygonPanel();
            }
            else if (_shouldDragWholePolygon)
            {
                var newLoc = mouseEventArgs.Location;
                var delta = PointHelper.Subtract(newLoc, _sourceOfPolygonMovement!.Value).ToSizeF();
                _polygon.MoveWholePolygon(delta);
                _sourceOfPolygonMovement = newLoc;
                _view.RefreshPolygonPanel();
            }
            else if (_controlPointBeingDragged != null)
            {
                _controlPointBeingDragged.MoveControlPoint(_polygon, mouseEventArgs.Location);
                _view.RefreshPolygonPanel();
            }
        }
    }

    private void PolygonPanelMouseDown(object? sender, EventArgs e)
    {
        var mouseEventArgs = (MouseEventArgs)e;
        switch (mouseEventArgs.Button)
        {
            case MouseButtons.Left:
                HandleLeftButtonDown(mouseEventArgs);
                break;
        }
    }

    private void FormKeyUp(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.ControlKey:
                _shouldDragWholePolygon = false;
                _sourceOfPolygonMovement = null;
                break;
        }
    }

    private void FormKeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.ControlKey:
                _shouldDragWholePolygon = true;
                break;
        }
    }

    private void VertexContextMenuClosing(object? sender, ToolStripDropDownClosingEventArgs e)
    {
        //if (e.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
        //    _contextMenusVertex = null;
    }

    private void EdgeContextMenuClosing(object? sender, ToolStripDropDownClosingEventArgs e)
    {
        //if (e.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
        //    _contextMenusEdge = null;
    }

    private void DeleteVertexClicked(object? sender, EventArgs e)
    {
        _polygon.DeleteVertex(_contextMenusVertex!);
        _contextMenusVertex = null;
        _view.RefreshPolygonPanel();
    }

    private void ContinuityC1Clicked(object? sender, EventArgs e)
        => ChangeVertexContinuityFromContextMenu(new C1Continuity(_polygon));

    private void ContinuityG1Clicked(object? sender, EventArgs e)
        => ChangeVertexContinuityFromContextMenu(new G1Continuity(_polygon));

    private void ContinuityG0Clicked(object? sender, EventArgs e)
        => ChangeVertexContinuityFromContextMenu(new G0Continuity());

    private void AddVertexClicked(object? sender, EventArgs e)
    {
        _polygon.AddVertex(_contextMenusEdge!);
        _contextMenusEdge = null;
        _view.RefreshPolygonPanel();
    }

    private void RemoveConstraintClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new NoConstraint());

    private void CircleArcClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new CircleArcEdgeConstraint(_polygon));

    private void BezierCurveClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new BezierCurveEdgeConstraint(_contextMenusEdge!, _polygon));

    private void FixedEdgeLengthClicked(object? sender, EventArgs e)
    {
        var length = _view.ShowFixedEdgeLengthForm(_polygon.GetEdgeLength(_contextMenusEdge!));
        if (length != null)
            ChangeConstraintFromContextMenu(new FixedEdgeLengthConstraint(length.Value));
        _contextMenusEdge = null;
    }

    private void DiagonalUpEdgeClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new DiagonalEdgeConstraint(DiagonalEdgeConstraint.DiagonalDirection.RightUp));

    private void DiagonalDownEdgeClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new DiagonalEdgeConstraint(DiagonalEdgeConstraint.DiagonalDirection.RightDown));

    private void HorizontalEdgeClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new HorizontalEdgeConstraint());

    private void HandleLeftButtonUp(MouseEventArgs e)
    {
        _vertexBeingDragged = null;
        _controlPointBeingDragged = null;
        _sourceOfPolygonMovement = null;
    }

    private void SharpBezierClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new SharpBezierEdgeConstraint(_contextMenusEdge!, _polygon));

    private void LoadPolygonClicked(object? sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        openFileDialog.Title = "Otwórz plik";
        openFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        openFileDialog.RestoreDirectory = true;
        openFileDialog.Multiselect = false;

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string sciezkaDoPliku = openFileDialog.FileName;
            try
            {
                string trescPliku = System.IO.File.ReadAllText(sciezkaDoPliku);
                var polygon = PolygonSerializer.DeserializePolygon(trescPliku);
                if (polygon != null)
                {
                    _polygon = polygon;
                    _view.RefreshPolygonPanel();
                    return;
                }
                _view.ShowMessageBox("Wczytano wielokąt pomyślnie!");
            }
            catch (Exception ex)
            {
                _view.ShowMessageBox("Wystąpił błąd podczas wczytywania pliku: " + ex.Message);
            }
        }
    }

    private void SavePolygonClicked(object? sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        saveFileDialog.Title = "Zapisz plik jako...";
        saveFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
        saveFileDialog.DefaultExt = "txt";
        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        saveFileDialog.RestoreDirectory = true;

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            string sciezkaDoPliku = saveFileDialog.FileName;
            try
            {
                string trescDoZapisu = PolygonSerializer.SerializePolygon(_polygon);
                System.IO.File.WriteAllText(sciezkaDoPliku, trescDoZapisu);

                _view.ShowMessageBox("Plik zapisany pomyślnie!");
            }
            catch (Exception ex)
            {
                _view.ShowMessageBox("Wystąpił błąd podczas zapisu pliku: " + ex.Message);
            }
        }
    }

    private void SubscribeToEvents()
    {
        _view.HelpClicked += HelpClicked;
        _view.PolygonResetClicked += PolygonResetClicked;
        _view.LibraryAlgorithmChosen += LibraryAlgorithmChosen;
        _view.BresenhamAlgorithmChosen += BresenhamAlgorithmChosen;
        _view.PolygonPanelPainting += PolygonPanelPainting;
        _view.PolygonPanelMouseDown += PolygonPanelMouseDown;
        _view.PolygonPanelMouseMove += PolygonPanelMouseMove;
        _view.PolygonPanelMouseUp += PolygonPanelMouseUp;
        _view.FormKeyDown += FormKeyDown;
        _view.FormKeyUp += FormKeyUp;
        _view.DeleteVertexClicked += DeleteVertexClicked;
        _view.ContinuityG0Clicked += ContinuityG0Clicked;
        _view.ContinuityG1Clicked += ContinuityG1Clicked;
        _view.ContinuityC1Clicked += ContinuityC1Clicked;
        _view.AddVertexClicked += AddVertexClicked;
        _view.HorizontalEdgeClicked += HorizontalEdgeClicked;
        _view.DiagonalUpEdgeClicked += DiagonalUpEdgeClicked;
        _view.DiagonalDownEdgeClicked += DiagonalDownEdgeClicked;
        _view.FixedEdgeLengthClicked += FixedEdgeLengthClicked;
        _view.BezierCurveClicked += BezierCurveClicked;
        _view.CircleArcClicked += CircleArcClicked;
        _view.RemoveConstraintClicked += RemoveConstraintClicked;
        _view.VertexContextMenuClosing += VertexContextMenuClosing;
        _view.EdgeContextMenuClosing += EdgeContextMenuClosing;
        _view.SharpBezierClicked += SharpBezierClicked;
        _view.SavePolygonClicked += SavePolygonClicked;
        _view.LoadPolygonClicked += LoadPolygonClicked;
    }
}
