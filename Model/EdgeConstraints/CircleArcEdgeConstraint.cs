using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;
using System.Numerics;

namespace PolygonEditor.Model.EdgeConstraints;

public class CircleArcEdgeConstraint : IEdgeConstraint
{
    private readonly Polygon _polygon;

    public CircleArcEdgeConstraint(Polygon polygon)
    {
        _polygon = polygon;
    }

    public EdgeType EdgeType
        => EdgeType.Arc;

    public string? Label
        => "Ł";

    public IVertexContinuity DefaultContinuity
        => new G0Continuity();

    public void ApplyConstraint(Vertex a, Vertex b)
    { }

    public bool CheckConstraint(Vertex a, Vertex b)
        => true;

    public (PointF Center, float Radius, float StartAngle, float SweepAngle) GetCircleParams(Vertex a, Vertex b)
    {
        ThrowIfVertexHasC1Continuity(a, b);
        PointF? center = null;
        float? radius = null;
        float? startAngle = null;
        float? sweepAngle = null;

        if (a.Continuity is G0Continuity && b.Continuity is G0Continuity)
        {
            center = ((a + b) / 2).ToPointF();
            radius = a.DistanceTo(b) / 2;
            startAngle = GetStartAngle(a, b);
            sweepAngle = 180;
        }
        else if (a.Continuity is G1Continuity g1Continuity && b.Continuity is G0Continuity)
        {
            var thisEdge = _polygon.GetEdgeBetween(a, b);
            var previousEdge = _polygon.GetOtherEdge(a, thisEdge);
            var tangentA = g1Continuity.GetContinuityConditions(a, previousEdge, thisEdge)!.Value.TangentVector;
            (center, radius) = CalculateCircle(a, b, tangentA);
            startAngle = GetStartAngle(new Vertex(center.Value.X, center.Value.Y), a);
            sweepAngle = GetSweepAngle(a, b, center.Value);
        }
        else if (a.Continuity is G0Continuity && b.Continuity is G1Continuity g1ContinuityB)
        {
            var thisEdge = _polygon.GetEdgeBetween(a, b);
            var nextEdge = _polygon.GetOtherEdge(b, thisEdge);
            var tangentB = g1ContinuityB.GetContinuityConditions(b, nextEdge, thisEdge)!.Value.TangentVector;
            tangentB.X *= -1;
            tangentB.Y *= -1;
            (center, radius) = CalculateCircle(b, a, tangentB);
            startAngle = GetStartAngle(new Vertex(center.Value.X, center.Value.Y), b);
            sweepAngle = GetSweepAngle(b, a, center.Value);
        }
        else
            throw new InvalidOperationException("Two arc vertices have G1 continuity!");

        return (center.Value, radius.Value, startAngle.Value, sweepAngle.Value);
    }

    private void ThrowIfVertexHasC1Continuity(Vertex a, Vertex b)
    {
        if (a.Continuity is C1Continuity || b.Continuity is C1Continuity)
            throw new InvalidOperationException("One of vertices has continuity type C1!");
    }

    private double ToDegrees(double radians)
        => radians * 180.0 / Math.PI;

    private float GetStartAngle(Vertex center, Vertex point)
    {
        // Funkcja oblicza kąt od osi X do wektora (point - center),
        // czyli początkowy kąt dla funkcji rysującej łuk.

        double dx = point.X - center.X;
        double dy = point.Y - center.Y;

        double radians = Math.Atan2(dy, dx);
        double degrees = ToDegrees(radians);
        if (degrees < 0)
            degrees += 360.0;

        return (float)degrees;
    }

    public float GetSweepAngle(Vertex a, Vertex b, PointF s)
    {
        // Funkcja oblicza kąt, dla jakiego ma być rysowany łuk okręgu.

        // Obliczenie wektorów promieniowych dla obu wierzchołków.
        double aX = a.X - s.X;
        double aY = a.Y - s.Y;
        double bX = b.X - s.X;
        double bY = b.Y - s.Y;

        // Obliczenie kątów dla każdego wektora
        double angleA = Math.Atan2(aY, aX);
        double angleB = Math.Atan2(bY, bX);

        // Obliczenie kąta jako różnicy i wybór mniejszego z kątów
        double sweepRadians = angleB - angleA;
        if (sweepRadians > Math.PI)
            sweepRadians -= 2 * Math.PI;
        else if (sweepRadians <= -Math.PI)
            sweepRadians += 2 * Math.PI;

        return (float)ToDegrees(sweepRadians);
    }

    private (PointF Center, float Radius) CalculateCircle(Vertex a, Vertex b, Vector2 tangentA)
    {
        // Funkcja oblicza środek i promień okręgu na podstawie dwóch wierzchołków a i b
        // oraz wektora stycznego w wierzchołku a, poprzez rozwiązanie układu równań:
        // T_x * x_S + T_y * y_S = C1 - równanie prostopadłej do wektora stycznego
        // (B_x - A_x) * x_S + (B_y - A_y) * y_S = C2 - równanie symetralnej prostej ab

        // Wyznaczenie współczynników w pierwszym równaniu
        // C1 wyznaczamy podstawiając punkt a do równania prostej
        double Tx = tangentA.X;
        double Ty = tangentA.Y;
        double C1 = Tx * a.X + Ty * a.Y;

        // Wyznaczenie współczynników w drugim równaniu
        // Wzór na C2 wynika z warunku na równą odległość między aS i Sb
        double DiffX = b.X - a.X;
        double DiffY = b.Y - a.Y;
        double C2 = 0.5 * (b.X * b.X - a.X * a.X + b.Y * b.Y - a.Y * a.Y);

        // Rozwiązanie układu równań metodą Cramera
        double D = Tx * DiffY - Ty * DiffX;
        double Dx = C1 * DiffY - Ty * C2;
        double Dy = Tx * C2 - C1 * DiffX;
        double Sx = Dx / D;
        double Sy = Dy / D;

        // Wyznaczenie środka i promienia
        PointF S = new PointF((float)Sx, (float)Sy);
        float radius = (float)Math.Sqrt((a.X - Sx) * (a.X - Sx) + (a.Y - Sy) * (a.Y - Sy));

        return (S, radius);
    }
}
