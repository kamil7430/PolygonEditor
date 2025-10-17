namespace PolygonEditor.Model.EdgeConstraints;

public class BezierCurveEdgeConstraint : IEdgeConstraint
{
    private readonly Edge _edge;
    public BezierCurveControlPoint Cp1 { get; set; }
    public BezierCurveControlPoint Cp2 { get; set; }

    public BezierCurveEdgeConstraint(Edge edge, Polygon polygon)
    {
        int offset = 50;
        _edge = edge;
        var (a, b) = polygon.GetEdgeVertices(edge);
        Cp1 = new BezierCurveControlPoint(a.X + offset, a.Y + offset, this);
        Cp2 = new BezierCurveControlPoint(b.X - offset, b.Y - offset, this);
    }

    public EdgeType EdgeType
        => EdgeType.BezierCurve;

    public string? Label
        => "B";

    public void ApplyConstraint(Vertex a, Vertex b)
    {

    }

    public bool CheckConstraint(Vertex a, Vertex b)
    {
        return true;
    }

    public void MoveControlPoint(BezierCurveControlPoint controlPoint, Polygon polygon, PointF destination)
    {
        controlPoint.MoveTo(destination);
        var (v1, v2) = polygon.GetEdgeVertices(_edge);
        if (controlPoint == Cp1)
            polygon.MoveVertex(v1, v1.ToPointF());
        else if (controlPoint == Cp2)
            polygon.MoveVertex(v2, v2.ToPointF());
        else
            throw new ArgumentException();
    }
}
