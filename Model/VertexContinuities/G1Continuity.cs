using System.Numerics;

namespace PolygonEditor.Model.VertexContinuities;

public class G1Continuity : G1C1ContinuitiesBase
{
    public override string? Label
        => "G1";

    public G1Continuity(Polygon polygon)
        : base(polygon)
    { }

    public override bool DoesAccept(EdgeType edge1Type, EdgeType edge2Type, IVertexContinuity vertex1Continuity, IVertexContinuity vertex2Continuity)
        => (edge1Type != EdgeType.Line || edge2Type != EdgeType.Line)
        && !(edge1Type == EdgeType.Arc && vertex1Continuity is G1Continuity)
        && !(edge2Type == EdgeType.Arc && vertex2Continuity is G1Continuity);

    public override (Vector2 TangentVector, bool ShouldLengthBeEqual)? GetContinuityConditions(Vertex vertex, Edge previousEdge, Edge currentEdge)
    {
        // TODO: check for bezier
        // Ten warunek zapobiega stack overflow przy ciągłości G1 dla dwóch sąsiadujących łuków.
        // Wyznacza wersor prostopadły do prostej wyznaczonej przez końce obu łuków.
        if (previousEdge.Constraint.EdgeType == EdgeType.Arc && currentEdge.Constraint.EdgeType == EdgeType.Arc)
        {
            var (v1, v2) = Polygon.GetEdgeVertices(previousEdge);
            var (v3, v4) = Polygon.GetEdgeVertices(currentEdge);
            Vertex a, c;

            if (v1 == v4)
                (a, c) = (v2, v3);
            else if (v2 == v3)
                (a, c) = (v1, v4);
            else
                throw new ArgumentException("Vertices are incorrect!");

            var ac = GetTangentVectorForLine(a, c, null!);
            var perpendicular = new Vector2(ac.Y, -ac.X);

            return (perpendicular, false);
        }

        return (GetTangentVector(vertex, previousEdge), false);
    }

    public override object Clone()
        => new G1Continuity(Polygon);
}
