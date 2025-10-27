using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.Dtos.BezierCurveUtils;
using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Dtos.EdgeConstraints;

public class BezierCurveEdgeConstraintDto : IEdgeConstraintDto
{
    public int EdgeIndex { get; set; }
    public BezierCurveControlPointDto Cp1 { get; set; }
    public BezierCurveControlPointDto Cp2 { get; set; }

    public BezierCurveEdgeConstraintDto(int edgeIndex, BezierCurveControlPoint cp1, BezierCurveControlPoint cp2)
    {
        EdgeIndex = edgeIndex;
        Cp1 = cp1.ToDto();
        Cp2 = cp2.ToDto();
    }

    public BezierCurveEdgeConstraintDto(int edgeIndex, BezierCurveControlPointDto cp1, BezierCurveControlPointDto cp2)
    {
        EdgeIndex = edgeIndex;
        Cp1 = cp1;
        Cp2 = cp2;
    }

    public virtual IEdgeConstraint GetConstraint(Polygon polygon)
    {
        var edge = polygon.Edges[EdgeIndex];
        var bezier = new BezierCurveEdgeConstraint(edge, polygon);
        bezier.Cp1 = Cp1.GetControlPoint(bezier);
        bezier.Cp2 = Cp2.GetControlPoint(bezier);
        return bezier;
    }
}
