using PolygonEditor.Model.Dtos;
using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model;

public class Edge
{
    public IEdgeConstraint Constraint { get; set; }

    public EdgeDto ToDto()
        => new EdgeDto(Constraint.ToDto());

    public Edge(IEdgeConstraint? constraint = null)
    {
        Constraint = constraint ?? new NoConstraint();
    }
}
