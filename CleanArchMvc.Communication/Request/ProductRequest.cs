using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CleanArchMvc.Communication.Request;

public class ProductRequest
{
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    [DisplayName("Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(5)]
    [MaxLength(200)]
    [DisplayName("Description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DisplayName("Price")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, 9999)]
    [DisplayName("Stock")]
    public int Stock { get; set; }

    [MaxLength(250)]
    [DisplayName("Product Image")]
    public string Image { get; set; } = string.Empty;

    [DisplayName("Categories")]
    public int CategoryId { get; set; }

    [JsonIgnore]
    public CategoryRequest? Category { get; set; }
}
