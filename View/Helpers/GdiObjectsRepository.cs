using System.Drawing.Drawing2D;

namespace PolygonEditor.View.Helpers;

public static class GdiObjectsRepository
{
    private static Pen? _lightGrayDashedPen;

    public static Pen LightGrayDashedPen
    {
        get
        {
            if (_lightGrayDashedPen == null)
            {
                _lightGrayDashedPen = new Pen(Color.LightGray);
                _lightGrayDashedPen.DashStyle = DashStyle.Dash;
                _lightGrayDashedPen.DashPattern = [10, 10];
            }
            return _lightGrayDashedPen;
        }
    }
}
