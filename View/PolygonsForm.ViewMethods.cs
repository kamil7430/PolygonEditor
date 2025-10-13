using PolygonEditor.View.Forms;

namespace PolygonEditor.View;

public partial class PolygonsForm
{
    public float VertexRadius { get; } = 10f;

    public void ShowMessageBox(string message)
        => MessageBox.Show(message);

    public void DrawLine(Graphics g, PointF p1, PointF p2)
        => g.DrawLine(Pens.Black, p1, p2);

    public void DrawArc(Graphics g, PointF center, float radius, float startAngle, float sweepAngle)
        => g.DrawArc(Pens.Black, center.X - radius, center.Y - radius,
            2 * radius, 2 * radius, startAngle, sweepAngle);

    public void DrawPixels(Graphics g, IEnumerable<PointF> points)
    {
        foreach (var p in points)
            g.FillRectangle(Brushes.Black, p.X, p.Y, 1, 1);
    }

    public void DrawVertex(Graphics g, PointF p)
        => g.FillEllipse(Brushes.PaleVioletRed, p.X - VertexRadius,
            p.Y - VertexRadius, VertexRadius * 2, VertexRadius * 2);

    public void DrawString(Graphics g, string text, PointF point)
        => g.DrawString(text, Font, Brushes.Violet, point);

    public void RefreshPolygonPanel()
        => polygonPanel.Refresh();

    public void ShowVertexContextMenu(PointF location)
    {
        var p = PointToScreen(polygonPanel.Location);
        p.Offset(new Point((int)location.X, (int)location.Y));
        vertexContextMenuStrip.Show(p);
    }

    public void ShowEdgeContextMenu(PointF location)
    {
        var p = PointToScreen(polygonPanel.Location);
        p.Offset(new Point((int)location.X, (int)location.Y));
        edgeContextMenuStrip.Show(p);
    }

    public float? ShowFixedEdgeLengthForm(float actualLength)
    {
        var form = new FixedEdgeLengthForm((decimal)actualLength);
        var result = form.ShowDialog();
        if (result == DialogResult.OK)
            return form.NewEdgeLength;
        return null;
    }
}
