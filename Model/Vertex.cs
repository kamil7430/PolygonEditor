namespace PolygonEditor.Model;

public class Vertex
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vertex(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point ToPoint()
        => new Point(X, Y);
}
