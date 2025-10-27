using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Dtos.EdgeConstraints;

public class BezierCurveEdgeConstraintDto : IEdgeConstraintDto
{
    public int EdgeIndex { get; set; }
    public BezierCurveControlPoint Cp1 { get; set; }
    public BezierCurveControlPoint Cp2 { get; set; }

    public BezierCurveEdgeConstraintDto(int edgeIndex, BezierCurveControlPoint cp1, BezierCurveControlPoint cp2)
    {
        EdgeIndex = edgeIndex;
        Cp1 = cp1;
        Cp2 = cp2;
    }

    public virtual IEdgeConstraint GetConstraint(Polygon polygon)
    {
        var edge = polygon.Edges[EdgeIndex];
        return new BezierCurveEdgeConstraint(edge, polygon);
    }
}
