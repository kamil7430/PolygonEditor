using PolygonEditor.Model;
using PolygonEditor.Model.EdgeConstraints;
using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.RenderingStrategies;
using PolygonEditor.View;

namespace PolygonEditor.Presenter;

public class PolygonsFormPresenter
{
    private const int DISTANCE_TO_CATCH_EDGE = 10;

    private Polygon _polygon;
    private Vertex? _vertexBeingDragged;

    private bool _shouldDragWholePolygon;
    private Point? _sourceOfPolygonMovement;

    private Vertex? _contextMenusVertex;
    private Edge? _contextMenusEdge;

    private readonly IPolygonEditorView _view;
    private IRenderingStrategy _renderingStrategy;

    public PolygonsFormPresenter(IPolygonEditorView view)
    {
        _polygon = Polygon.Predefined;
        _shouldDragWholePolygon = false;
        _view = view;
        _renderingStrategy = new LibraryDrawingFunctionStrategy();
        SubscribeToEvents();
    }

    private void HelpClicked(object? sender, EventArgs e)
        => _view.ShowMessageBox("To jest pomoc.");

    private void LibraryAlgorithmChosen(object? sender, EventArgs e)
        => _renderingStrategy = new LibraryDrawingFunctionStrategy();

    private void BresenhamAlgorithmChosen(object? sender, EventArgs e)
        => _renderingStrategy = new MyBresenhamAlgorithmStrategy();

    private void PolygonPanelPainting(object? sender, EventArgs e)
    {
        if (e is PaintEventArgs paintEventArgs)
        {
            var pixels = DrawLines(paintEventArgs.Graphics);
            if (pixels != null)
                _view.DrawPixels(paintEventArgs.Graphics, pixels);
            DrawVertices(paintEventArgs.Graphics);
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
                var delta = PointHelper.Subtract(newLoc, _sourceOfPolygonMovement!.Value).ToSize();
                _polygon.MoveWholePolygon(delta);
                _sourceOfPolygonMovement = newLoc;
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

    private void AddVertexClicked(object? sender, EventArgs e)
    {
        _polygon.AddVertex(_contextMenusEdge!);
        _contextMenusEdge = null;
        _view.RefreshPolygonPanel();
    }

    private void RemoveConstraintClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new NoConstraint());

    private void CircleArcClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new CircleArcEdgeConstraint());

    private void BezierCurveClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new BezierCurveEdgeConstraint());

    private void FixedEdgeLengthClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new FixedEdgeLengthConstraint(100));

    private void DiagonalEdgeClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new DiagonalEdgeConstraint());

    private void HorizontalEdgeClicked(object? sender, EventArgs e)
        => ChangeConstraintFromContextMenu(new HorizontalEdgeConstraint());

    private IEnumerable<Point>? DrawLines(Graphics g)
    {
        if (!_renderingStrategy.ShouldUseLibraryDrawingFunction)
            return _renderingStrategy.GetPixelsToPaint(_polygon);
        var vertices = _polygon.Vertices;
        int vertexCount = vertices.Count;
        for (int i = 0; i < vertexCount; i++)
            _view.DrawLine(g, vertices[i].ToPoint(), vertices[(i + 1) % vertexCount].ToPoint());
        return null;
    }

    private void DrawVertices(Graphics g)
    {
        foreach (var vertex in _polygon.Vertices)
            _view.DrawVertex(g, vertex.ToPoint());
    }

    private void HandleLeftButtonUp(MouseEventArgs e)
    {
        _vertexBeingDragged = null;
        _sourceOfPolygonMovement = null;
    }

    private void HandleRightButtonUp(MouseEventArgs e)
    {
        var vertex = _polygon.GetNearestVertexInRadius(e.Location, _view.VertexRadius);
        if (vertex != null)
        {
            _contextMenusVertex = vertex;
            _view.ShowVertexContextMenu(e.Location);
            return;
        }
        var edge = _polygon.GetNearestEdgeInRadius(e.Location, DISTANCE_TO_CATCH_EDGE);
        if (edge != null)
        {
            _contextMenusEdge = edge;
            _view.ShowEdgeContextMenu(e.Location);
        }
    }

    private void HandleLeftButtonDown(MouseEventArgs e)
    {
        if (_vertexBeingDragged != null)
            return;
        if (_shouldDragWholePolygon)
            _sourceOfPolygonMovement = e.Location;
        else
            _vertexBeingDragged = _polygon.GetNearestVertexInRadius(e.Location, _view.VertexRadius);
    }

    private void ChangeConstraintFromContextMenu(IEdgeConstraint constraint)
    {
        _contextMenusEdge!.Constraint = constraint;
        _polygon.ApplyConstraints(_polygon.Edges.IndexOf(_contextMenusEdge));
        _contextMenusEdge = null;
        _view.RefreshPolygonPanel();
    }

    private void SubscribeToEvents()
    {
        _view.HelpClicked += HelpClicked;
        _view.LibraryAlgorithmChosen += LibraryAlgorithmChosen;
        _view.BresenhamAlgorithmChosen += BresenhamAlgorithmChosen;
        _view.PolygonPanelPainting += PolygonPanelPainting;
        _view.PolygonPanelMouseDown += PolygonPanelMouseDown;
        _view.PolygonPanelMouseMove += PolygonPanelMouseMove;
        _view.PolygonPanelMouseUp += PolygonPanelMouseUp;
        _view.FormKeyDown += FormKeyDown;
        _view.FormKeyUp += FormKeyUp;
        _view.DeleteVertexClicked += DeleteVertexClicked;
        _view.AddVertexClicked += AddVertexClicked;
        _view.HorizontalEdgeClicked += HorizontalEdgeClicked;
        _view.DiagonalEdgeClicked += DiagonalEdgeClicked;
        _view.FixedEdgeLengthClicked += FixedEdgeLengthClicked;
        _view.BezierCurveClicked += BezierCurveClicked;
        _view.CircleArcClicked += CircleArcClicked;
        _view.RemoveConstraintClicked += RemoveConstraintClicked;
        _view.VertexContextMenuClosing += VertexContextMenuClosing;
        _view.EdgeContextMenuClosing += EdgeContextMenuClosing;
    }
}
