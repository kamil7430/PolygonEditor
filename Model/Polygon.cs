using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.EdgeConstraints;
using PolygonEditor.Model.Helpers;
using PolygonEditor.Model.VertexContinuities;
using System.Numerics;

namespace PolygonEditor.Model;

public class Polygon
{
    public List<Vertex> Vertices { get; set; }
    public List<Edge> Edges { get; set; }

    public Polygon(List<Vertex> vertices, List<Edge> edges)
    {
        if (vertices.Count != edges.Count)
            throw new ArgumentException("Vertex count does not match edge count!");
        Vertices = vertices;
        Edges = edges;
    }

    public static Polygon Predefined
    {
        get
        {
            var polygon = new Polygon
            (
                [new(30, 15), new(530, 130), new(440, 425), new(75, 330), new(235, 135)],
                [new(), new(), new(), new(), new()]
            );
            polygon.TryApplyConstraints(polygon.Edges[1], new CircleArcEdgeConstraint(polygon));
            polygon.TryApplyConstraints(polygon.Edges[3], new DiagonalEdgeConstraint(DiagonalEdgeConstraint.DiagonalDirection.RightUp));
            polygon.TryApplyConstraints(polygon.Edges[2], new BezierCurveEdgeConstraint(polygon.Edges[2], polygon));
            return polygon;
        }
    }

    public Vertex MoveVertex(Vertex vertex, PointF destination)
    {
        // Funkcja wywoływana, gdy użytkownik chce przesunąć wierzchołek. Oprócz
        // zmiany pozycji w modelu, dodatkowo wywoływany jest Solver, który
        // rozwiązuje ograniczenia, bądź informuje o niemożności takiej operacji.

        int vertexIndex = Vertices.IndexOf(vertex);
        var delta = destination.Subtract(vertex.ToPointF()).ToSizeF();
        if (!ConstraintSolver.TryMoveVertexAndApplyConstraints(this, vertex, destination))
            MoveWholePolygon(delta);
        return Vertices[vertexIndex];
    }

    public void MoveBezierCurveControlPoint(BezierCurveControlPoint controlPoint, Vertex vertex,
        BezierCurveEdgeConstraint bezierConstraint, Edge bezierEdge, PointF destination)
    {
        // Obsługuje przesuwanie punktu kontrolnego krzywej Beziera. Wykorzystuje 'BezierConstraintSolver'
        // do wstępnego przeliczenia pozycji (z uwzględnieniem ciągłości),
        // a następnie 'ConstraintSolver' do walidacji i zastosowania zmian w kontekście całego wielokąta.
        // W razie niepowodzenia cofa zmiany i przesuwa cały wielokąt.

        var delta = destination.Subtract(controlPoint.ToPointF()).ToSizeF();

        var firstEdgeToMove = GetOtherEdge(vertex, bezierEdge);
        var vertexToMove = GetOtherVertex(firstEdgeToMove, vertex);
        var (oldControlPoint, oldVertex, oldVertexToMove) = BezierConstraintSolver.MoveFirstEdge(this,
            controlPoint, vertex, vertexToMove, bezierConstraint, bezierEdge, firstEdgeToMove, destination);

        if (!ConstraintSolver.TryMoveVertexAndApplyConstraints(this, vertexToMove, vertexToMove.ToPointF(), false))
        {
            (controlPoint.X, controlPoint.Y) = (oldControlPoint.X, oldControlPoint.Y);
            (vertex.X, vertex.Y) = (oldVertex.X, oldVertex.Y);
            (vertexToMove.X, vertexToMove.Y) = (oldVertexToMove.X, oldVertexToMove.Y);
            MoveWholePolygon(delta);
        }
    }

    public void MoveWholePolygon(SizeF delta)
    {
        // Przesuwa cały wielokąt (wszystkie wierzchołki oraz punkty kontrolne krzywych Beziera)
        // o podany wektor (delta).

        Vertices.ForEach(v => v.Offset(delta));
        foreach (var edge in Edges.Where(e => e.Constraint.EdgeType == EdgeType.BezierCurve))
        {
            var bezier = (BezierCurveEdgeConstraint)edge.Constraint;
            bezier.Cp1.Offset(delta);
            bezier.Cp2.Offset(delta);
        }
    }

    public bool TryApplyConstraints(Edge edge, IEdgeConstraint constraint)
    {
        // Próbuje zastosować nowe ograniczenie (constraint) do wybranej krawędzi.
        // Sprawdza specyficzne warunki (np. sąsiednie ograniczenia poziome),
        // próbuje ustawić domyślną ciągłość wierzchołków (lub G0) i wywołuje Solver,
        // aby sprawdzić, czy zmiana jest możliwa. W razie niepowodzenia cofa wszystkie zmiany.

        var oldConstraint = edge.Constraint;
        edge.Constraint = constraint;
        var index = Edges.IndexOf(edge);

        if (constraint is HorizontalEdgeConstraint &&
            (Edges[(index - 1).TrueModulo(Edges.Count)].Constraint is HorizontalEdgeConstraint
            || Edges[(index + 1).TrueModulo(Edges.Count)].Constraint is HorizontalEdgeConstraint))
        {
            edge.Constraint = oldConstraint;
            return false;
        }

        var oldContinuity = Vertices[index].Continuity;
        var oldContinuityNext = Vertices[(index + 1).TrueModulo(Vertices.Count)].Continuity;
        if (!TryApplyVertexContinuity(Vertices[index], constraint.DefaultContinuity, Vertices[(index + 1).TrueModulo(Vertices.Count)]))
        {
            if (!TryApplyVertexContinuity(Vertices[index], new G0Continuity(), Vertices[(index + 1).TrueModulo(Vertices.Count)]))
            {
                edge.Constraint = oldConstraint;
                return false;
            }
        }

        if (!ConstraintSolver.TryMoveVertexAndApplyConstraints(this, Vertices[index], Vertices[index].ToPointF()))
        {
            Vertices[index].Continuity = oldContinuity;
            Vertices[(index + 1).TrueModulo(Vertices.Count)].Continuity = oldContinuityNext;
            edge.Constraint = oldConstraint;
            return false;
        }

        return true;
    }

    public bool TryApplyVertexContinuity(Vertex ver1, IVertexContinuity continuity, Vertex? ver2 = null)
    {
        // Próbuje zastosować nową regułę ciągłości do jednego lub dwóch wierzchołków. 
        // Sprawdza, czy typy sąsiednich krawędzi i ciągłość sąsiednich wierzchołków są akceptowane
        // przez nową regułę. Na koniec waliduje zmianę za pomocą Solvera.

        var (e1, e2) = GetVertexEdges(ver1);
        Edge? e3 = null, e4 = null;
        if (ver2 != null)
            (e3, e4) = GetVertexEdges(ver2);

        int index1 = Vertices.IndexOf(ver1);
        int? index2 = null;
        if (ver2 != null)
            index2 = Vertices.IndexOf(ver2);

        if (ver2 == null)
        {
            var v1 = Vertices[(index1 - 1).TrueModulo(Vertices.Count)];
            var v2 = Vertices[(index1 + 1).TrueModulo(Vertices.Count)];
            if (!continuity.DoesAccept(e1.Constraint.EdgeType, e2.Constraint.EdgeType, v1.Continuity, v2.Continuity))
                return false;
        }
        else
        {
            if (index1 > index2!.Value && !(index1 == Vertices.Count - 1 && index2.Value == 0))
            {
                (index1, index2) = (index2.Value, index1);
                (e1, e2, e3, e4) = (e3, e4, e1, e2);
            }
            var ver0 = Vertices[(index1 - 1).TrueModulo(Vertices.Count)];
            var ver3 = Vertices[(index2.Value + 1).TrueModulo(Vertices.Count)];

            if (!continuity.DoesAccept(e1!.Constraint.EdgeType, e2!.Constraint.EdgeType, ver0.Continuity, continuity))
                return false;
            if (!continuity.DoesAccept(e3!.Constraint.EdgeType, e4!.Constraint.EdgeType, continuity, ver3.Continuity))
                return false;
        }

        var oldContinuity = ver1.Continuity;
        var oldContinuityNext = ver2?.Continuity;
        ver1.Continuity = continuity;
        if (ver2 != null)
            ver2.Continuity = continuity;
        if (!ConstraintSolver.TryMoveVertexAndApplyConstraints(this, ver1, ver1.ToPointF()))
        {
            ver1.Continuity = oldContinuity;
            if (ver2 != null)
                ver2.Continuity = oldContinuityNext!;
            return false;
        }

        return true;
    }

    public void DeleteVertex(Vertex vertex)
    {
        // Usuwa wierzchołek z wielokąta. Usuwa również powiązaną z nim krawędź i resetuje ograniczenia oraz ciągłość
        // na sąsiednich krawędziach i wierzchołkach do stanu domyślnego (brak ograniczeń, ciągłość G0).

        var index = Vertices.IndexOf(vertex);
        Vertices.RemoveAt(index);
        Edges.RemoveAt(index);
        index %= Edges.Count;
        Edges[(index - 1).TrueModulo(Edges.Count)].Constraint = new NoConstraint();
        Edges[index].Constraint = new NoConstraint();
        Vertices[(index - 1).TrueModulo(Edges.Count)].Continuity = new G0Continuity();
        Vertices[index].Continuity = new G0Continuity();
    }

    public void AddVertex(Edge edge)
    {
        // Dodaje nowy wierzchołek pośrodku wybranej krawędzi, efektywnie dzieląc ją na dwie. 
        // Resetuje ograniczenie na oryginalnej krawędzi oraz ciągłość na jej wierzchołkach
        // i wstawia nowy wierzchołek i nową krawędź.

        var index = Edges.IndexOf(edge);
        var v1 = Vertices[index];
        var v2 = Vertices[(index + 1) % Vertices.Count];
        var newVertex = (v1 + v2) / 2;
        edge.Constraint = new NoConstraint();
        v1.Continuity = new G0Continuity();
        v2.Continuity = new G0Continuity();
        Vertices.Insert(index + 1, newVertex);
        Edges.Insert(index + 1, new Edge());
    }

    public Vertex? GetNearestVertexInRadius(PointF point, float radius)
    {
        // Wyszukuje najbliższy wierzchołek do danego punktu, pod warunkiem, że znajduje się on w określonym promieniu (radius).
        // Zwraca 'null', jeśli żaden wierzchołek nie spełnia kryterium. Używa przeszukiwania liniowego.

        (Vertex Vertex, float Distance)? nearestVertex = null;
        Vector2 p = point.ToVector2();
        foreach (var vertex in Vertices)
        {
            Vector2 v = vertex.ToVector2();
            var distance = Vector2.Distance(p, v);
            if (distance <= radius)
                if (nearestVertex == null || distance < nearestVertex.Value.Distance)
                    nearestVertex = (vertex, distance);
        }
        return nearestVertex?.Vertex;
    }

    public BezierCurveControlPoint? GetNearestBezierCurveControlPointInRadius(PointF point, float radius)
    {
        // Wyszukuje najbliższy punkt kontrolny krzywej Beziera do danego punktu, w określonym promieniu.
        // Przeszukuje tylko krawędzie z ograniczeniem 'BezierCurveEdgeConstraint'.

        (BezierCurveControlPoint ControlPoint, float Distance)? nearestControlPoint = null;
        Vector2 p = point.ToVector2();
        foreach (var bezierCurve in Edges.Where(e => e.Constraint.EdgeType == EdgeType.BezierCurve))
        {
            var constraint = (BezierCurveEdgeConstraint)bezierCurve.Constraint;
            var (cp1, cp2) = (constraint.Cp1, constraint.Cp2);
            var (dist1, dist2) = (Vector2.Distance(p, cp1.ToVector2()), Vector2.Distance(p, cp2.ToVector2()));
            if (dist1 <= radius)
                if (nearestControlPoint == null || dist1 < nearestControlPoint.Value.Distance)
                    nearestControlPoint = (cp1, dist1);
            if (dist2 <= radius)
                if (nearestControlPoint == null || dist2 < nearestControlPoint.Value.Distance)
                    nearestControlPoint = (cp2, dist2);
        }
        return nearestControlPoint?.ControlPoint;
    }

    public Edge? GetNearestEdgeInRadius(PointF p, float radius)
    {
        // Wyszukuje najbliższą krawędź do danego punktu (p) w określonym promieniu. 
        // Oblicza odległość prostopadłą punktu od linii zdefiniowanej przez wierzchołki krawędzi.
        // Zwraca krawędź z najmniejszą odległością, jeśli jest ona mniejsza niż 'radius'.

        (Edge Edge, double Distance)? nearestEdge = null;
        var verticesCount = Vertices.Count;
        for (int i = 0; i < verticesCount; i++)
        {
            Vertex a = Vertices[i];
            Vertex b = Vertices[(i + 1) % verticesCount];
            var numerator = Math.Abs((b.X - a.X) * (a.Y - p.Y) - (a.X - p.X) * (b.Y - a.Y));
            var denominator = Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
            var distance = numerator / denominator;
            if (distance <= radius)
                if (nearestEdge == null || distance < nearestEdge.Value.Distance)
                    nearestEdge = (Edges[i], distance);
        }
        return nearestEdge?.Edge;
    }

    public Edge GetEdgeBetween(Vertex v1, Vertex v2)
    {
        // Zwraca krawędź znajdującą się pomiędzy dwoma podanymi wierzchołkami.
        // Rzuca wyjątek, jeśli wierzchołki nie są bezpośrednimi sąsiadami.

        int index1 = Vertices.IndexOf(v1);
        int index2 = Vertices.IndexOf(v2);
        if (index1 > index2)
            (index1, index2) = (index2, index1);

        if (index2 - index1 != 1 && !(index2 == Vertices.Count - 1 && index1 == 0))
            throw new ArgumentException("These vertices are not neighbours!");

        if (index2 == Vertices.Count - 1 && index1 == 0)
            return Edges[index2];
        return Edges[index1];
    }

    public (Vertex V1, Vertex V2) GetEdgeVertices(Edge edge)
    {
        // Zwraca krotkę (V1, V2) zawierającą dwa wierzchołki, które definiują podaną krawędź.

        var index = Edges.IndexOf(edge);
        return (Vertices[index], Vertices[(index + 1) % Vertices.Count]);
    }

    public (Edge E1, Edge E2) GetVertexEdges(Vertex vertex)
    {
        // Zwraca krotkę (E1, E2) zawierającą dwie krawędzie, które spotykają się
        // w podanym wierzchołku (krawędź wchodząca i wychodząca).

        var index = Vertices.IndexOf(vertex);
        return (Edges[(index - 1).TrueModulo(Vertices.Count)], Edges[index]);
    }

    public Edge GetOtherEdge(Vertex vertex, Edge edge)
    {
        // Dla danego wierzchołka i jednej z jego krawędzi, zwraca tę drugą krawędź
        // połączoną z tym wierzchołkiem.

        var (e1, e2) = GetVertexEdges(vertex);
        return edge == e1 ? e2 : e1;
    }

    public Vertex GetOtherVertex(Edge edge, Vertex vertex)
    {
        // Dla danej krawędzi i jednego z jej wierzchołków, zwraca ten drugi
        // wierzchołek należący do tej krawędzi.

        var (v1, v2) = GetEdgeVertices(edge);
        return vertex == v1 ? v2 : v1;
    }

    // Zwraca krawędź poprzedzającą krawędź (a, b) z perspektywy wierzchołka 'a'.
    public Edge GetPreviousEdge(Vertex a, Vertex b)
        => GetOtherEdge(a, GetEdgeBetween(a, b));

    // Zwraca krawędź następującą po krawędzi (a, b) z perspektywy wierzchołka 'b'.
    public Edge GetNextEdge(Vertex a, Vertex b)
        => GetOtherEdge(b, GetEdgeBetween(a, b));

    public float GetEdgeLength(Edge edge)
    {
        // Oblicza długość (odległość euklidesową) podanej krawędzi, bazując
        // na pozycjach jej dwóch wierzchołków.

        int index = Edges.IndexOf(edge);
        int nextIndex = (index + 1).TrueModulo(Edges.Count);
        return Vertices[index].DistanceTo(Vertices[nextIndex]);
    }
}
