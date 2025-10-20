using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;
using System.Numerics;

namespace PolygonEditor.Model.EdgeConstraints;

public class BezierCurveEdgeConstraint : IEdgeConstraint
{
    private readonly Edge _edge;
    private readonly Polygon _polygon;
    public BezierCurveControlPoint Cp1 { get; set; }
    public BezierCurveControlPoint Cp2 { get; set; }

    public BezierCurveEdgeConstraint(Edge edge, Polygon polygon)
    {
        int offset = 50;
        _edge = edge;
        _polygon = polygon;
        var (a, b) = polygon.GetEdgeVertices(edge);
        Cp1 = new BezierCurveControlPoint(a.X + offset, a.Y + offset, this);
        Cp2 = new BezierCurveControlPoint(b.X - offset, b.Y - offset, this);
    }

    public EdgeType EdgeType
        => EdgeType.BezierCurve;

    public string? Label
        => "B";

    public IVertexContinuity DefaultContinuity
        => new G1Continuity(_polygon);

    public void ApplyConstraint(Vertex a, Vertex b)
    {
        // Zastosowanie ograniczeń w przypadku ruszenia czymś innym niż punktem kontrolnym.

        var previousEdge = _polygon.GetPreviousEdge(a, b);
        var thisEdge = _polygon.GetEdgeBetween(a, b);
        var nextEdge = _polygon.GetNextEdge(a, b);
        var conditionsA = a.Continuity.GetContinuityConditions(a, previousEdge, thisEdge);
        var conditionsB = b.Continuity.GetContinuityConditions(b, nextEdge, thisEdge);

        if (conditionsA != null)
        {
            var cp = GetCorrespondingControlPoint(a);

            if (conditionsA.Value.ShouldLengthBeEqual)
            {
                cp.X = a.X + conditionsA.Value.TangentVector.X / 3;
                cp.Y = a.Y + conditionsA.Value.TangentVector.Y / 3;
            }
            else
            {
                var vector = conditionsA.Value.TangentVector;
                float length = a.DistanceTo(cp);
                float scale = length / vector.Length();
                vector.X *= scale;
                vector.Y *= scale;
                cp.X = a.X + vector.X;
                cp.Y = a.Y + vector.Y;
            }
        }
        if (conditionsB != null)
        {
            var cp = GetCorrespondingControlPoint(b);

            if (conditionsB.Value.ShouldLengthBeEqual)
            {
                cp.X = b.X + conditionsB.Value.TangentVector.X / 3;
                cp.Y = b.Y + conditionsB.Value.TangentVector.Y / 3;
            }
            else
            {
                var vector = conditionsB.Value.TangentVector;
                float length = b.DistanceTo(cp);
                float scale = length / vector.Length();
                vector.X *= scale;
                vector.Y *= scale;
                cp.X = b.X + vector.X;
                cp.Y = b.Y + vector.Y;
            }
        }
    }

    public bool CheckConstraint(Vertex a, Vertex b)
    {
        var previousEdge = _polygon.GetPreviousEdge(a, b);
        var thisEdge = _polygon.GetEdgeBetween(a, b);
        var nextEdge = _polygon.GetNextEdge(a, b);
        var conditionsA = a.Continuity.GetContinuityConditions(a, previousEdge, thisEdge);
        var conditionsB = b.Continuity.GetContinuityConditions(b, nextEdge, thisEdge);

        if (conditionsA != null)
        {
            var cp = GetCorrespondingControlPoint(a);
            var frontTangentVector = GetTangentVector(a, cp);

            if (conditionsA.Value.ShouldLengthBeEqual)
            {
                if (!(frontTangentVector.X.IsEqual(conditionsA.Value.TangentVector.X)
                    && frontTangentVector.Y.IsEqual(conditionsA.Value.TangentVector.Y)))
                    return false;
            }
            else
            {
                var v_actual = frontTangentVector;
                var v_target = conditionsA.Value.TangentVector;

                // Sprawdzenie, czy wektory skierowane są w tym samym kierunku
                float dotProduct = v_actual.X * v_target.X + v_actual.Y * v_target.Y;
                if (dotProduct <= 0)
                    return false;

                // Sprawdzenie współliniowości przy użyciu iloczynu wektorowego
                float crossProduct = v_actual.X * v_target.Y - v_actual.Y * v_target.X;
                if (!crossProduct.IsEqual(0.0f))
                    return false;
            }
        }
        if (conditionsB != null)
        {
            var cp = GetCorrespondingControlPoint(b);
            var backTangentVector = GetTangentVector(b, cp);

            if (conditionsB.Value.ShouldLengthBeEqual)
            {
                if (!(backTangentVector.X.IsEqual(conditionsB.Value.TangentVector.X)
                    && backTangentVector.Y.IsEqual(conditionsB.Value.TangentVector.Y)))
                    return false;
            }
            else
            {
                var v_actual = backTangentVector;
                var v_target = conditionsB.Value.TangentVector;

                // Sprawdzenie, czy wektory skierowane są w tym samym kierunku
                float dotProduct = v_actual.X * v_target.X + v_actual.Y * v_target.Y;
                if (dotProduct <= 0)
                    return false;

                // Sprawdzenie współliniowości przy użyciu iloczynu wektorowego
                float crossProduct = v_actual.X * v_target.Y - v_actual.Y * v_target.X;
                if (!crossProduct.IsEqual(0.0f))
                    return false;
            }
        }

        return true;
    }

    public void ApplyBezierNeighbourConstraint(BezierCurveControlPoint oldControlPoint, BezierCurveControlPoint controlPoint,
        Vertex a, Vertex b, Vector2 tangentVector, bool shouldLengthBeEqual)
    {
        var cp = GetCorrespondingControlPoint(a);
        if (shouldLengthBeEqual)
        {
            cp.X = a.X + tangentVector.X / 3;
            cp.Y = a.Y + tangentVector.Y / 3;
        }
        else
        {
            float length = a.DistanceTo(cp);
            float scale = length / tangentVector.Length();
            tangentVector.X *= scale;
            tangentVector.Y *= scale;
            cp.X = a.X + tangentVector.X;
            cp.Y = a.Y + tangentVector.Y;
        }
    }

    private Vector2 GetTangentVector(Vertex vertex, BezierCurveControlPoint controlPoint)
        => ((vertex - controlPoint.ToVertex()) * 3).ToVector2();

    public BezierCurveControlPoint GetCorrespondingControlPoint(Vertex vertex)
    {
        var (v1, v2) = _polygon.GetEdgeVertices(_edge);
        if (vertex == v1)
            return Cp1;
        else if (vertex == v2)
            return Cp2;
        else
            throw new ArgumentException("This vertex is not on this edge!");
    }

    public void MoveControlPoint(BezierCurveControlPoint controlPoint, Polygon polygon, PointF destination)
    {
        // Funkcja poruszająca punktem kontrolnym - ta operacja ma oddzielny mechanizm aplikowania
        // ograniczeń, ze względu na konieczność modyfikowania nie swoich wierzchołków.

        var (v1, v2) = polygon.GetEdgeVertices(_edge);
        if (controlPoint == Cp1)
            polygon.MoveBezierCurveControlPoint(Cp1, v1, this, _edge, destination);
        else if (controlPoint == Cp2)
            polygon.MoveBezierCurveControlPoint(Cp2, v2, this, _edge, destination);
        else
            throw new ArgumentException("Control points don't match!");
    }

    public IEnumerable<PointF> GetPixelsToPaint(Vertex a, Vertex b)
    {
        // Funkcja wyznacza piksele do zamalowania, żeby narysować krzywą
        // Beziera trzeciego stopnia.

        // Ujednolicenie typów w celu zwiększenia czytelności kodu 
        // i zwiększenia stabilności numerycznej.
        (PointD v0, PointD v1) = (a.ToPointD(), Cp1.ToPointD());
        (PointD v2, PointD v3) = (Cp2.ToPointD(), b.ToPointD());

        // Przejście do bazy potęgowej: P(𝑡) = 𝐴_3 ∙ 𝑡^3 + 𝐴_2 ∙ 𝑡^2 + 𝐴_1 ∙ 𝑡 + 𝐴_0.
        var A_0 = v0;
        var A_1 = (v1 - v0) * 3;
        var A_2 = (v2 - (v1 * 2) + v0) * 3;
        var A_3 = v3 - (v2 * 3) + (v1 * 3) - v0;

        // Definicja różnic progresywnych
        double density = 1;
        double approximateLength = v0.DistanceTo(v1) + v1.DistanceTo(v2) + v2.DistanceTo(v3);
        double steps = approximateLength * density;
        if (steps < 100)
            steps = 100;
        double d = 1 / steps;
        var delta0P = A_0;
        var delta1P = ((((A_3 * d) + A_2) * d) + A_1) * d;
        var delta2P = ((A_3 * (3 * d)) + A_2) * (2 * d * d);
        var delta3P = A_3 * (6 * d * d * d);

        // Obliczanie kolejnych punktów w pętli
        yield return delta0P.ToPointF();
        for (double i = 0; i <= 1; i += d)
        {
            delta2P += delta3P;
            delta1P += delta2P;
            delta0P += delta1P;
            yield return delta0P.ToPointF();
        }
    }
}
