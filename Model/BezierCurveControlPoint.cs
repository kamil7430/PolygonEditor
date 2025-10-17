using PolygonEditor.Model.EdgeConstraints;
using System.Numerics;

namespace PolygonEditor.Model;

public class BezierCurveControlPoint : ICastableToVector2, ICloneable
{
    private readonly BezierCurveEdgeConstraint _constraint;
    public float X { get; set; }
    public float Y { get; set; }

    public BezierCurveControlPoint(float x, float y, BezierCurveEdgeConstraint constraint)
    {
        X = x;
        Y = y;
        _constraint = constraint;
    }

    public PointF ToPointF()
        => new PointF(X, Y);

    public Vector2 ToVector2()
        => new Vector2(X, Y);

    public void MoveControlPoint(Polygon polygon, PointF destination)
        => _constraint.MoveControlPoint(this, polygon, destination);

    public object Clone()
        => new BezierCurveControlPoint(X, Y, _constraint);
}
