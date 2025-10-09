namespace PolygonEditor.View;

public partial class PolygonsForm
{
    public int VertexRadius { get; } = 10;

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
        => g.FillEllipse(Brushes.PaleVioletRed, p.X - VertexRadius,
            p.Y - VertexRadius, VertexRadius * 2, VertexRadius * 2);

    public void RefreshPolygonPanel()
        => polygonPanel.Refresh();
}
