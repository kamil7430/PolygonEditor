using System.Numerics;

namespace PolygonEditor.Model.Helpers;

public static class VertexHelper
{
    public static void ParallelToVectorKeepingLength(Vertex a, Vertex b, Vector2 tangentVector)
    {
        float length = a.DistanceTo(b);
        float scale = length / tangentVector.Length();
        tangentVector.X *= scale;
        tangentVector.Y *= scale;
        b.X = a.X + tangentVector.X;
        b.Y = a.Y + tangentVector.Y;
    }
}
