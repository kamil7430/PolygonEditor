using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Dtos.EdgeConstraints;

public class CircleArcEdgeConstraintDto : IEdgeConstraintDto
{
    public IEdgeConstraint GetConstraint(Polygon polygon)
        => new CircleArcEdgeConstraint(polygon);
}
