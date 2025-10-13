using System.Drawing.Drawing2D;

namespace PolygonEditor.View.Helpers;

public static class GdiObjectsRepository
{
    private static Pen? _blackDashedPen;

    public static Pen BlackDashedPen
    {
        get
        {
            if (_blackDashedPen == null)
            {
                _blackDashedPen = new Pen(Color.Black);
                _blackDashedPen.DashStyle = DashStyle.Dash;
                _blackDashedPen.DashPattern = [10, 10];
            }
            return _blackDashedPen;
        }
    }
}
