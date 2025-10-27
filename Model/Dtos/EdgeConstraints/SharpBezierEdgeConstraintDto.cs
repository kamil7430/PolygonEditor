using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Dtos.EdgeConstraints;

public class SharpBezierEdgeConstraintDto : BezierCurveEdgeConstraintDto
{
    public SharpBezierEdgeConstraintDto(int edgeIndex, BezierCurveControlPoint cp1, BezierCurveControlPoint cp2)
        : base(edgeIndex, cp1, cp2)
    { }

    public override IEdgeConstraint GetConstraint(Polygon polygon)
    {
        var edge = polygon.Edges[EdgeIndex];
        return new SharpBezierEdgeConstraint(edge, polygon);
    }
}
