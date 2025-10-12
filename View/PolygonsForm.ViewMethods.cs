using PolygonEditor.View.Forms;

namespace PolygonEditor.View;

public partial class PolygonsForm
{
    public int VertexRadius { get; } = 10;

    public void ShowMessageBox(string message)
        => MessageBox.Show(message);

    public void DrawLine(Graphics g, Point p1, Point p2)
        => g.DrawLine(Pens.Black, p1, p2);

    public void DrawArc(Graphics g, Point center, int radius, float startAngle, float sweepAngle)
        => g.DrawArc(Pens.Black, center.X - radius, center.Y - radius,
            2 * radius, 2 * radius, startAngle, sweepAngle);

    public void DrawPixels(Graphics g, IEnumerable<Point> points)
    {
        foreach (var p in points)
            g.FillRectangle(Brushes.Black, p.X, p.Y, 1, 1);
    }

    public void DrawVertex(Graphics g, Point p)
        => g.FillEllipse(Brushes.PaleVioletRed, p.X - VertexRadius,
            p.Y - VertexRadius, VertexRadius * 2, VertexRadius * 2);

    public void DrawString(Graphics g, string text, Point point)
        => g.DrawString(text, Font, Brushes.Violet, point);

    public void RefreshPolygonPanel()
        => polygonPanel.Refresh();

    public void ShowVertexContextMenu(Point location)
    {
        var p = PointToScreen(polygonPanel.Location);
        p.Offset(location);
        vertexContextMenuStrip.Show(p);
    }

    public void ShowEdgeContextMenu(Point location)
    {
        var p = PointToScreen(polygonPanel.Location);
        p.Offset(location);
        edgeContextMenuStrip.Show(p);
    }

    public int? ShowFixedEdgeLengthForm(int actualLength)
    {
        var form = new FixedEdgeLengthForm(actualLength);
        var result = form.ShowDialog();
        if (result == DialogResult.OK)
            return form.NewEdgeLength;
        return null;
    }
}
