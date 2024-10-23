namespace CleanArchMvc.Communication.Response;

public class UserTokenResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}