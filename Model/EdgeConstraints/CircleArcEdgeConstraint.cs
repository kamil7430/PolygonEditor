using PolygonEditor.Model.Helpers;

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
        if (a.ContinuityType == ContinuityType.G0 && b.ContinuityType == ContinuityType.G0)
            return;
        if (a.ContinuityType == ContinuityType.G0 && b.ContinuityType == ContinuityType.G1)
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
        if (a.ContinuityType == ContinuityType.G0 && b.ContinuityType == ContinuityType.G0)
            return true;
        if (a.ContinuityType == ContinuityType.G0 && b.ContinuityType == ContinuityType.G1)
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

        if (a.ContinuityType == ContinuityType.G0 && b.ContinuityType == ContinuityType.G0)
        {
            center = ((a + b) / 2).ToPointF();
            radius = a.DistanceTo(b) / 2;
            if (a.X.IsEqual(b.X))
                startAngle = 90;
            else
                startAngle = (float)ToDegrees(Math.Atan((b.Y - a.Y) / (b.X - a.X)));
            sweepAngle = 180;
        }
        else if (a.ContinuityType == ContinuityType.G0 && b.ContinuityType == ContinuityType.G1)
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
        if (a.ContinuityType == ContinuityType.C1 || b.ContinuityType == ContinuityType.C1)
            throw new InvalidOperationException("One of vertices has continuity type C1!");
    }

    private double ToDegrees(double radians)
        => radians * 180.0 / Math.PI;
}
