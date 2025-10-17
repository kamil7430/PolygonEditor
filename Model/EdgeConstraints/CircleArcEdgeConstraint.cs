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
            sweepAngle = (float)ToDegrees(GetCentralAngle(a, b, center.Value));
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
            sweepAngle = (float)ToDegrees(GetCentralAngle(b, a, center.Value));
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
        // Oblicza kąt od osi X do wektora (point - center) za pomocą Atan2
        double dx = point.X - center.X;
        double dy = point.Y - center.Y;

        // Math.Atan2(y, x) zwraca kąt w RADIANACH w zakresie (-pi, pi]
        double radians = Math.Atan2(dy, dx);

        // Konwersja na stopnie
        double degrees = ToDegrees(radians);

        // Zapewnienie, że kąt jest w zakresie [0, 360) (opcjonalne, ale zalecane)
        if (degrees < 0)
            degrees += 360.0;

        return (float)degrees;
    }

    public float GetCentralAngle(Vertex a, Vertex b, PointF s)
    {
        // 1. Obliczenie wektorów promieniowych
        double aX = a.X - s.X;
        double aY = a.Y - s.Y;

        double bX = b.X - s.X;
        double bY = b.Y - s.Y;

        // 2. Obliczenie kątów dla każdego wektora za pomocą Atan2
        // Math.Atan2(y, x) zwraca kąt w radianach od osi X w zakresie (-PI, PI]
        double angleA = Math.Atan2(aY, aX);
        double angleB = Math.Atan2(bY, bX);

        // 3. Obliczenie kąta zamiatania (sweep) jako różnicy
        double sweepRadians = angleB - angleA;

        // 4. Normalizacja kąta do zakresu (-PI, PI]
        // (Gdyby np. A = 170 stopni, B = -170 stopni, różnica wyniosłaby -340 stopni.
        // Chcemy zamiast tego krótszą drogę, czyli +20 stopni).
        if (sweepRadians > Math.PI)
        {
            sweepRadians -= 2 * Math.PI;
        }
        else if (sweepRadians <= -Math.PI)
        {
            sweepRadians += 2 * Math.PI;
        }

        return (float)sweepRadians;
    }

    private (PointF Center, float Radius) CalculateCircle(Vertex a, Vertex b, Vector2 tangentA)
    {
        // --- Równanie 1: Prosta prostopadła do stycznej w punkcie A ---
        // Na tej prostej musi leżeć środek S = (xS, yS).
        // T_x * xS + T_y * yS = C1
        double Tx = tangentA.X;
        double Ty = tangentA.Y;
        double C1 = Tx * a.X + Ty * a.Y;

        // --- Równanie 2: Symetralna odcinka AB ---
        // Na tej prostej musi leżeć środek S = (xS, yS).
        // (Bx - Ax) * xS + (By - Ay) * yS = C2
        double DiffX = b.X - a.X;
        double DiffY = b.Y - a.Y;
        double C2 = 0.5 * (b.X * b.X - a.X * a.X + b.Y * b.Y - a.Y * a.Y);

        // Rozwiązanie układu równań liniowych:
        // Tx * xS + Ty * yS = C1
        // DiffX * xS + DiffY * yS = C2

        // Obliczanie wyznacznika głównego (D)
        double D = Tx * DiffY - Ty * DiffX;

        // --- Sprawdzenie, czy linie są równoległe (D = 0) ---
        //if (Math.Abs(D) < 1e-9) // Używamy tolerancji dla porównań zmiennoprzecinkowych
        //{
        //    // Oznacza to, że wektor styczny T_A jest współliniowy z wektorem AB.
        //    // Jedynym rozwiązaniem jest łuk o nieskończonym promieniu (odcinek AB).
        //    return new ArcParameters { IsValid = false, Error = "T_A jest współliniowy z A->B (nieskończony promień/prosta)." };
        //}

        // --- Obliczanie współrzędnych środka S (z użyciem reguły Cramera) ---

        // Wyznacznik Dx
        double Dx = C1 * DiffY - Ty * C2;
        // Wyznacznik Dy
        double Dy = Tx * C2 - C1 * DiffX;

        double Sx = Dx / D;
        double Sy = Dy / D;

        PointF S = new PointF((float)Sx, (float)Sy);

        // --- Obliczanie promienia R ---
        float radius = (float)Math.Sqrt((a.X - Sx) * (a.X - Sx) + (a.Y - Sy) * (a.Y - Sy));

        return (S, radius);
    }
}
