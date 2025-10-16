using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;

namespace PolygonEditor.Model.EdgeConstraints;

public class CircleArcEdgeConstraint : IEdgeConstraint
{
    public EdgeType EdgeType
        => EdgeType.Arc;

    public string? Label
        => "Ł";

    public void ApplyConstraint(Vertex a, Vertex b)
    {
        ThrowIfVertexHasC1Continuity(a, b);
        if (a.Continuity is G0Continuity || b.Continuity is G0Continuity)
            return;
        if (a.Continuity is G1Continuity && b.Continuity is G0Continuity)
        {
            // TODO
        }
        else
        {
            // TODO
        }
    }

    public bool CheckConstraint(Vertex a, Vertex b)
    {
        ThrowIfVertexHasC1Continuity(a, b);
        if (a.Continuity is G0Continuity || b.Continuity is G0Continuity)
            return true;
        if (a.Continuity is G1Continuity && b.Continuity is G0Continuity)
        {
            // TODO
            return false;
        }
        else
        {
            // TODO
            return false;
        }
    }

    public (PointF Center, float Radius, float StartAngle, float SweepAngle) GetCircleParams(Vertex a, Vertex b)
    {
        ThrowIfVertexHasC1Continuity(a, b);
        PointF? center = null;
        float? radius = null;
        float? startAngle = null;
        float? sweepAngle = null;

        if (a.Continuity is G0Continuity || b.Continuity is G0Continuity)
        {
            center = ((a + b) / 2).ToPointF();
            radius = a.DistanceTo(b) / 2;
            if (a.X.IsEqual(b.X))
                startAngle = 90;
            else
                startAngle = (float)ToDegrees(Math.Atan((b.Y - a.Y) / (b.X - a.X)));
            sweepAngle = 180;
        }
        else if (a.Continuity is G1Continuity && b.Continuity is G0Continuity)
        {
            // TODO
            throw new NotImplementedException();
        }
        else
        {
            // TODO
            throw new NotImplementedException();
        }

        return (center.Value, radius.Value, startAngle.Value, sweepAngle.Value);
    }

    private void ThrowIfVertexHasC1Continuity(Vertex a, Vertex b)
    {
        if (a.Continuity is C1Continuity || b.Continuity is C1Continuity)
            throw new InvalidOperationException("One of vertices has continuity type C1!");
    }

    private double ToDegrees(double radians)
        => radians * 180.0 / Math.PI;
}
