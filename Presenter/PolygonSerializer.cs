using PolygonEditor.Model;
using PolygonEditor.Model.Dtos;
using System.Text.Json;

namespace PolygonEditor.Presenter;

public static class PolygonSerializer
{
    public static string SerializePolygon(Polygon polygon)
    {
        var dto = polygon.ToDto();
        return JsonSerializer.Serialize(dto);
    }

    public static Polygon? DeserializePolygon(string json)
    {
        var dto = JsonSerializer.Deserialize<PolygonDto>(json);
        return dto?.GetPolygon();
    }
}
