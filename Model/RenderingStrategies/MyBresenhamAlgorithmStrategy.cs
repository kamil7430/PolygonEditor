namespace PolygonEditor.Model.RenderingStrategies;

public class MyBresenhamAlgorithmStrategy : IRenderingStrategy
{
    public bool ShouldUseLibraryDrawingFunction
        => false;

    public IEnumerable<PointF> GetPixelsToPaint(Polygon polygon, IEnumerable<Edge> edgesToPaint)
    {
        foreach (var edge in edgesToPaint)
        {
            var (v1, v2) = polygon.GetEdgeVertices(edge);

            // Obsłużenie upierdliwego przypadku (tan zbiega do nieskończoności)
            if (Math.Abs(v1.X - v2.X) < 1)
            {
                if (v1.Y > v2.Y)
                    (v1, v2) = (v2, v1);
                for (int y = (int)v1.Y; y < v2.Y; y++)
                    yield return new PointF(v1.X, y);
            }

            // Zapewnienie sobie krawędzi "w prawo"
            if (v1.X > v2.X)
                (v1, v2) = (v2, v1);

            // Obliczenie nachylenia prostej i wybór odpowiedniej transformacji pod algorytm
            var tan = (v2.Y - v1.Y) / (v2.X - v1.X);
            if (tan > 1)
            {
                // Zamiana osi
                var pixels = BresenhamAlgorithm((int)v1.Y, (int)v1.X, (int)v2.Y, (int)v2.X);
                foreach (var p in pixels)
                    yield return new PointF(p.Y, p.X);
            }
            else if (tan > 0)
            {
                // Brak transformacji - przypadek bazowy
                var pixels = BresenhamAlgorithm((int)v1.X, (int)v1.Y, (int)v2.X, (int)v2.Y);
                foreach (var p in pixels)
                    yield return p;
            }
            else if (tan > -1)
            {
                // Odbicie względem osi OX
                var pixels = BresenhamAlgorithm((int)v1.X, (int)-v1.Y, (int)v2.X, (int)-v2.Y);
                foreach (var p in pixels)
                    yield return new PointF(p.X, -p.Y);
            }
            else
            {
                // Zamiana osi i odbicie względem OX
                var pixels = BresenhamAlgorithm((int)v2.Y, (int)-v2.X, (int)v1.Y, (int)-v1.X);
                foreach (var p in pixels)
                    yield return new PointF(-p.Y, p.X);
            }
        }
    }

    private List<PointF> BresenhamAlgorithm(int x1, int y1, int x2, int y2)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;
        int d = 2 * dy - dx;
        int incrE = 2 * dy;
        int incrNE = 2 * (dy - dx);
        int x = x1;
        int y = y1;
        List<PointF> pixels = [new PointF(x, y)];

        while (x < x2)
        {
            if (d < 0)
            {
                d += incrE;
                x++;
            }
            else
            {
                d += incrNE;
                x++;
                y++;
            }
            pixels.Add(new PointF(x, y));
        }

        return pixels;
    }
}
