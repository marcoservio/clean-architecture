using System.Text.Json.Serialization;

namespace CleanArchMvc.Communication.Response;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Image { get; private set; } = string.Empty;

    public int CategoryId { get; set; }

    [JsonIgnore]
    public CategoryResponse? Category { get; set; }
}
