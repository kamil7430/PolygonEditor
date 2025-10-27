using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;

namespace PolygonEditor.Model.EdgeConstraints;

public class SharpBezierEdgeConstraint : BezierCurveEdgeConstraint
{
    public SharpBezierEdgeConstraint(Edge edge, Polygon polygon) : base(edge, polygon)
    {
        int offset = 100;
        var (v1, v2) = polygon.GetEdgeVertices(edge);
        if (v2.Y.IsEqual(v1.Y))
        {
            Cp1.X = v2.X;
            Cp1.Y = v1.Y + offset;
            Cp2.X = v1.X;
            Cp2.Y = v2.Y + offset;
        }
        else if (v2.X.IsEqual(v1.X))
        {
            Cp1.X = v1.X + offset;
            Cp1.Y = v2.Y;
            Cp2.X = v2.X + offset;
            Cp2.Y = v1.Y;
        }
        else
        {
            float m = -1 / ((v2.Y - v1.Y) / (v2.X - v1.X));
            var deltaX = (float)(offset / Math.Sqrt(1 + m * m));
            var deltaY = (float)(m * offset / Math.Sqrt(1 + m * m));
            Cp1.X = v2.X + deltaX;
            Cp1.Y = v2.Y + deltaY;
            Cp2.X = v1.X + deltaX;
            Cp2.Y = v1.Y + deltaY;
        }
    }

    public override string? Label
        => "SB";

    public override IVertexContinuity DefaultContinuity
        => new G0Continuity();
}
