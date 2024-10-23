using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Communication.Request;

public class CategoryRequest
{
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
