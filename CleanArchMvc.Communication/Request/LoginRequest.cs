using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CleanArchMvc.Communication.Request;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [JsonIgnore]
    public string ReturnUrl { get; set; } = string.Empty;
}
