namespace PolygonEditor.View;

public partial class PolygonsForm
{
    private const int VERTEX_RADIUS = 20;

    public void ShowMessageBox(string message)
        => MessageBox.Show(message);

    public void DrawLine(Graphics g, Point p1, Point p2)
        => g.DrawLine(Pens.Black, p1, p2);

    public void DrawPixels(Graphics g, IEnumerable<Point> points)
    {
        foreach (var p in points)
            g.FillRectangle(Brushes.Black, p.X, p.Y, 1, 1);
    }

    public void DrawVertex(Graphics g, Point p)
        => g.FillEllipse(Brushes.PaleVioletRed, p.X - VERTEX_RADIUS / 2,
            p.Y - VERTEX_RADIUS / 2, VERTEX_RADIUS, VERTEX_RADIUS);
}
