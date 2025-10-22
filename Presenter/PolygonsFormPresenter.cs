using PolygonEditor.Model;
using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.EdgeConstraints;
using PolygonEditor.Model.RenderingStrategies;
using PolygonEditor.Model.VertexContinuities;
using PolygonEditor.View;

namespace PolygonEditor.Presenter;

public partial class PolygonsFormPresenter
{
    // Klasa prezentera pośredniczy między widokiem a modelem. Znajdują się
    // tutaj metody obsługujące zdarzenia widoku, najczęściej delegując pracę
    // dalej do modelu w odpowiedni sposób, np. z dodatkowymi parametrami.

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
            _view.DrawPixels(g, bezierConstraint.GetPixelsToPaint(v1, v2));
        }
    }

    private void DrawVertices(Graphics g)
    {
        foreach (var vertex in _polygon.Vertices)
            _view.DrawVertex(g, vertex.ToPointF(), vertex.Continuity.Label);
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
}
