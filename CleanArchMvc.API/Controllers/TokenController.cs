using CleanArchMvc.Communication.Request;
using CleanArchMvc.Communication.Response;
using CleanArchMvc.Domain.Account;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController(IAuthenticate authenticate, IConfiguration configuration) : ControllerBase
{
    private readonly IAuthenticate _authenticate = authenticate;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authenticate.Authenticate(request.Email, request.Password);

        if (result)
            return Ok(GenerateToken(request));

        return BadRequest();
    }

    private UserTokenResponse GenerateToken(LoginRequest request)
    {
        var claims = new[]
        {
            new Claim("email", request.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddMinutes(10);

        JwtSecurityToken token = new(
            issuer: _configuration["Jwt:Issuer"]!,
            audience: _configuration["Jwt:Audience"]!,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new UserTokenResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }

    [HttpPost("CreateUser")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> CreateUser([FromBody] RegisterRequest request)
    {
        var result = await _authenticate.RegisterUser(request.Email, request.Password);

        if (result)
            return Ok();

        return BadRequest();
    }
}
