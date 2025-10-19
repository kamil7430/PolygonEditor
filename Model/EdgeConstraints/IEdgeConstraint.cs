using PolygonEditor.Model.VertexContinuities;
using System.Numerics;

namespace PolygonEditor.Model.EdgeConstraints;

public interface IEdgeConstraint
{
    EdgeType EdgeType { get; }
    string? Label { get; }
    IVertexContinuity DefaultContinuity { get; }
    bool CheckConstraint(Vertex a, Vertex b);
    void ApplyConstraint(Vertex a, Vertex b);
    void ApplyBezierNeighbourConstraint(Vertex a, Vertex b, Vector2 tangentVector, bool shouldLengthBeEqual);
}
