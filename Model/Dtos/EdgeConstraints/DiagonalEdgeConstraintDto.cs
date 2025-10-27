using PolygonEditor.Model.EdgeConstraints;

namespace PolygonEditor.Model.Dtos.EdgeConstraints;

public class DiagonalEdgeConstraintDto : IEdgeConstraintDto
{
    public DiagonalEdgeConstraint.DiagonalDirection Direction { get; set; }
    public bool ChangeX { get; set; }

    public DiagonalEdgeConstraintDto(DiagonalEdgeConstraint.DiagonalDirection direction, bool changeX)
    {
        Direction = direction;
        ChangeX = changeX;
    }

    public IEdgeConstraint GetConstraint(Polygon polygon)
        => new DiagonalEdgeConstraint(Direction);
}
