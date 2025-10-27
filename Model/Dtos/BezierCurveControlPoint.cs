using PolygonEditor.Model.BezierCurveUtils;
using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Dtos.BezierCurveUtils;

public class BezierCurveControlPointDto
{
    public float X { get; set; }
    public float Y { get; set; }

    public BezierCurveControlPointDto(float x, float y)
    {
        X = x;
        Y = y;
    }

    public BezierCurveControlPoint GetControlPoint(BezierCurveEdgeConstraint edgeConstraint)
        => new BezierCurveControlPoint(X, Y, edgeConstraint);
}
