using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Dtos.EdgeConstraints;

public class HorizontalEdgeConstraintDto : IEdgeConstraintDto
{
    public IEdgeConstraint GetConstraint(Polygon polygon)
        => new HorizontalEdgeConstraint();
}
