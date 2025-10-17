using PolygonEditor.Model.VertexContinuities;
using System.Numerics;

namespace PolygonEditor.Model;

public class Vertex : ICastableToVector2, ICloneable
{
    public float X { get; set; }
    public float Y { get; set; }
    public IVertexContinuity Continuity { get; set; }

    public Vertex(float x, float y, IVertexContinuity? continuity = null)
    {
        X = x;
        Y = y;
        Continuity = continuity ?? new G0Continuity();
    }

    public void MoveTo(PointF point)
    {
        X = point.X;
        Y = point.Y;
    }

    public void Offset(SizeF size)
    {
        X += size.Width;
        Y += size.Height;
    }

    public static Vertex operator +(Vertex a, Vertex b)
        => new Vertex(a.X + b.X, a.Y + b.Y);

    public static Vertex operator -(Vertex a, Vertex b)
        => new Vertex(a.X - b.X, a.Y - b.Y);

    public static Vertex operator *(Vertex a, float b)
        => new Vertex(a.X * b, a.Y * b, (IVertexContinuity)a.Continuity.Clone());

    public static Vertex operator /(Vertex a, float b)
        => new Vertex(a.X / b, a.Y / b, (IVertexContinuity)a.Continuity.Clone());

    public PointF ToPointF()
        => new PointF(X, Y);

    public Vector2 ToVector2()
        => new Vector2(X, Y);

    public PointD ToPointD()
        => new PointD(X, Y);

    public object Clone()
        => new Vertex(X, Y, (IVertexContinuity)Continuity.Clone());
}
