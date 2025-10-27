using PolygonEditor.Model.Dtos.EdgeConstraints;

namespace PolygonEditor.Model.Dtos;

public class EdgeDto
{
    public IEdgeConstraintDto Constraint { get; set; }

    public EdgeDto(IEdgeConstraintDto constraint)
    {
        this.Constraint = constraint;
    }
}
