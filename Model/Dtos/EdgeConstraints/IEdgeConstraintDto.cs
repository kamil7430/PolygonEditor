using PolygonEditor.Model.EdgeConstraints;
using System.Text.Json.Serialization;

namespace PolygonEditor.Model.Dtos.EdgeConstraints;

[JsonDerivedType(typeof(BezierCurveEdgeConstraintDto), 0)]
[JsonDerivedType(typeof(CircleArcEdgeConstraintDto), 1)]
[JsonDerivedType(typeof(DiagonalEdgeConstraintDto), 2)]
[JsonDerivedType(typeof(FixedEdgeLengthConstraintDto), 3)]
[JsonDerivedType(typeof(HorizontalEdgeConstraintDto), 4)]
[JsonDerivedType(typeof(NoConstraintDto), 5)]
[JsonDerivedType(typeof(SharpBezierEdgeConstraintDto), 6)]
public interface IEdgeConstraintDto
{
    IEdgeConstraint GetConstraint(Polygon polygon);
}
