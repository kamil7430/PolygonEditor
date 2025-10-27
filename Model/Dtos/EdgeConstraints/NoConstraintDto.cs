using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Dtos.EdgeConstraints;

public class NoConstraintDto : IEdgeConstraintDto
{
    public IEdgeConstraint GetConstraint(Polygon polygon)
        => new NoConstraint();
}
