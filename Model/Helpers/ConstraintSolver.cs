using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Helpers;

public static class ConstraintSolver
{
    public static bool TryMoveVertexAndApplyConstraints(Polygon polygon, Vertex vertexMoved, PointF destination, bool skipBack = false)
    {
        // Główna metoda Solvera. Próbuje przesunąć wierzchołek (vertexMoved) w nowe miejsce (destination)
        // i zastosować ograniczenia. Klonuje stan wierzchołków, wykonuje próbę przesunięcia i wywołuje TryApplyConstraints.
        // Jeśli zastosowanie ograniczeń się nie powiedzie, przywraca poprzedni stan wierzchołków.

        int movedVertexIndex = polygon.Vertices.IndexOf(vertexMoved);
        var vertices = polygon.Vertices.Clone();
        polygon.Vertices[movedVertexIndex].MoveTo(destination);

        if (TryApplyConstraints(polygon.Vertices, polygon.Edges, movedVertexIndex, skipBack))
            return true;

        polygon.Vertices = vertices;
        return false;
    }

    private static bool TryApplyConstraints(List<Vertex> vertices, List<Edge> edges, int movedVertexIndex, bool skipBack)
    {
        // Prywatna metoda implementująca logikę propagacji ograniczeń. Rozpoczyna od przesuniętego wierzchołka
        // (movedVertexIndex) i iteracyjnie przemieszcza się po wielokącie "do przodu"
        // (zgodnie z ruchem wskazówek zegara), sprawdzając i nakładając ograniczenia na kolejne krawędzie,
        // dopóki nie wróci do punktu startu lub nie napotka spełnionego ograniczenia.
        // Jeśli 'skipBack' jest 'false', powtarza proces "do tyłu" (przeciwnie do ruchu wskazówek zegara).

        int vertexCount = vertices.Count;
        int i = movedVertexIndex, j = (i + 1).TrueModulo(vertexCount);

        // Propagacja ograniczeń "do przodu"
        while (!edges[i].Constraint.CheckConstraint(vertices[i], vertices[j]))
        {
            edges[i].Constraint.ApplyConstraint(vertices[i], vertices[j]);
            i = j;
            j = (j + 1).TrueModulo(vertexCount);

            // Zamknięcie pętli
            if (j == movedVertexIndex)
                break;
        }

        // Specjalna obsługa krzywej Beziera na końcu propagacji "do przodu"
        if (j != movedVertexIndex && edges[j].Constraint is BezierCurveEdgeConstraint)
            edges[j].Constraint.ApplyConstraint(vertices[j], vertices[(j + 1).TrueModulo(vertexCount)]);

        if (skipBack)
        {
            if (j == movedVertexIndex)
                return false;
            return true;
        }

        // Zapisujemy, gdzie skończyła się propagacja "do przodu"
        int lastTouchedIndex = i;
        i = movedVertexIndex;
        j = (i - 1).TrueModulo(vertexCount);

        // Propagacja ograniczeń "wstecz"
        while (!edges[(i - 1).TrueModulo(vertexCount)].Constraint.CheckConstraint(vertices[i], vertices[j]))
        {
            edges[(i - 1).TrueModulo(vertexCount)].Constraint.ApplyConstraint(vertices[i], vertices[j]);
            i = j;
            j = (j - 1).TrueModulo(vertexCount);

            // Jeśli propagacja "wstecz" dotarła do miejsca, które zmieniła propagacja "do przodu", mamy konflikt
            if (lastTouchedIndex == i)
                return false;
        }

        // Specjalna obsługa krzywej Beziera na końcu propagacji "wstecz"
        if (edges[(j - 1).TrueModulo(vertexCount)].Constraint is BezierCurveEdgeConstraint)
            edges[(j - 1).TrueModulo(vertexCount)].Constraint.ApplyConstraint(vertices[j], vertices[(j - 1).TrueModulo(vertexCount)]);

        return true;
    }
}
