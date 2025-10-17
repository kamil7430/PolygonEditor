namespace PolygonEditor.Model.EdgeConstraints;

public class BezierCurveEdgeConstraint : IEdgeConstraint
{
    public BezierCurveControlPoint Cp1 { get; set; }
    public BezierCurveControlPoint Cp2 { get; set; }

    public BezierCurveEdgeConstraint()
    {
        Cp1 = new BezierCurveControlPoint(50, 50, this);
        Cp2 = new BezierCurveControlPoint(150, 150, this);
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
    { throw new NotImplementedException(); }
}
