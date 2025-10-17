using PolygonEditor.Model;
using PolygonEditor.Model.EdgeConstraints;
using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.RenderingStrategies;
using PolygonEditor.Model.VertexContinuities;
using PolygonEditor.View;

namespace PolygonEditor.Presenter;

public class PolygonsFormPresenter
{
    private const float DISTANCE_TO_CATCH_EDGE = 10f;

    private Polygon _polygon;
    private Vertex? _vertexBeingDragged;
    private BezierCurveControlPoint? _controlPointBeingDragged;

    private bool _shouldDragWholePolygon;
    private PointF? _sourceOfPolygonMovement;

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
        => ChangeConstraintFromContextMenu(new BezierCurveEdgeConstraint());

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

    private IEnumerable<PointF>? DrawLines(Graphics g, IEnumerable<Edge> edges)
    {
        if (!_renderingStrategy.ShouldUseLibraryDrawingFunction)
            return _renderingStrategy.GetPixelsToPaint(_polygon, edges);
        foreach (var edge in edges)
        {
            var (v1, v2) = _polygon.GetEdgeVertices(edge);
            _view.DrawLine(g, v1.ToPointF(), v2.ToPointF());
        }
        return null;
    }

    private void DrawArcs(Graphics g, IEnumerable<Edge> edges)
    {
        foreach (var edge in edges)
        {
            var (v1, v2) = _polygon.GetEdgeVertices(edge);
            var (center, radius, startAngle, sweepAngle) = ((CircleArcEdgeConstraint)edge.Constraint).GetCircleParams(v1, v2);
            _view.DrawArc(g, center, radius, startAngle, sweepAngle);
            _view.DrawDashedLine(g, v1.ToPointF(), v2.ToPointF());
        }
    }

    private void DrawBezierCurves(Graphics g, IEnumerable<Edge> edges)
    {
        foreach (var edge in edges)
        {
            var (v1, v2) = _polygon.GetEdgeVertices(edge);
            var bezierConstraint = (BezierCurveEdgeConstraint)edge.Constraint;
            var (cp1, cp2) = (bezierConstraint.Cp1, bezierConstraint.Cp2);
            _view.DrawDashedLine(g, v1.ToPointF(), v2.ToPointF());
            _view.DrawDashedLine(g, v1.ToPointF(), cp1.ToPointF());
            _view.DrawDashedLine(g, cp1.ToPointF(), cp2.ToPointF());
            _view.DrawDashedLine(g, cp2.ToPointF(), v2.ToPointF());
            _view.DrawControlPoint(g, cp1.ToPointF());
            _view.DrawControlPoint(g, cp2.ToPointF());
        }
    }

    private void DrawVertices(Graphics g)
    {
        foreach (var vertex in _polygon.Vertices)
            _view.DrawVertex(g, vertex.ToPointF());
    }

    private void DrawStrings(Graphics g)
    {
        foreach (var edge in _polygon.Edges)
        {
            if (edge.Constraint.Label == null)
                continue;
            var (v1, v2) = _polygon.GetEdgeVertices(edge);
            var point = ((v1 + v2) / 2).ToPointF();
            _view.DrawString(g, edge.Constraint.Label, point);
        }
    }

    private void HandleLeftButtonUp(MouseEventArgs e)
    {
        _vertexBeingDragged = null;
        _controlPointBeingDragged = null;
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
        if (_vertexBeingDragged != null || _controlPointBeingDragged != null)
            return;
        if (_shouldDragWholePolygon)
            _sourceOfPolygonMovement = e.Location;
        else
        {
            _vertexBeingDragged = _polygon.GetNearestVertexInRadius(e.Location, _view.VertexRadius);
            if (_vertexBeingDragged == null)
                _controlPointBeingDragged = _polygon.GetNearestBezierCurveControlPointInRadius(e.Location, _view.VertexRadius);
        }
    }

    private void ChangeConstraintFromContextMenu(IEdgeConstraint constraint)
    {
        if (!_polygon.TryApplyConstraints(_contextMenusEdge!, constraint))
            _view.ShowMessageBox("Ograniczenie nie może być dodane!");
        _contextMenusEdge = null;
        _view.RefreshPolygonPanel();
    }

    private void ChangeVertexContinuityFromContextMenu(IVertexContinuity continuity)
    {
        if (!_polygon.TryApplyVertexContinuity(_contextMenusVertex!, continuity))
            _view.ShowMessageBox("Wybrana ciągłość w wierzchołku nie może zostać zastosowana!");
        _contextMenusVertex = null;
        _view.RefreshPolygonPanel();
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
    }
}
