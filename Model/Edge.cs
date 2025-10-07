using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model;

internal class Edge
{
    public IEdgeConstraint Constraint { get; set; }

    public Edge(IEdgeConstraint? constraint = null)
    {
        Constraint = constraint ?? new NoConstraint();
    }
}
