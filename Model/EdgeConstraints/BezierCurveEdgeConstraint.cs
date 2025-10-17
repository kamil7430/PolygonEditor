using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;

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

    }

    public bool CheckConstraint(Vertex a, Vertex b)
    {
        return true;
    }

    public void MoveControlPoint(BezierCurveControlPoint controlPoint, Polygon polygon, PointF destination)
    {
        // TODO: prawdopodobnie trzeba będzie wydzielić nową funkcję w Polygonie
        controlPoint.MoveTo(destination);
        var (v1, v2) = polygon.GetEdgeVertices(_edge);
        if (controlPoint == Cp1)
            polygon.MoveVertex(v1, v1.ToPointF());
        else if (controlPoint == Cp2)
            polygon.MoveVertex(v2, v2.ToPointF());
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
