using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Dtos.EdgeConstraints;

public class FixedEdgeLengthConstraintDto : IEdgeConstraintDto
{
    public float Length { get; set; }

    public FixedEdgeLengthConstraintDto(float length)
    {
        Length = length;
    }

    public IEdgeConstraint GetConstraint(Polygon polygon)
        => new FixedEdgeLengthConstraint(Length);
}
