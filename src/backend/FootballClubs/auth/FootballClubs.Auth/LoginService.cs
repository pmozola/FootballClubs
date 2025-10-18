using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FootballClubs.Auth;

public class LoginService(UserManager<IdentityUser> userManager
, SignInManager<IdentityUser> signInManager, IJwtKeyService keyService) : ILoginService
{
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new LoginResponse(new LoginResponseResult(false, "Not Found"));
        }
        
        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyService.GetKey()),
                    SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginResponse(new LoginResponseResult(true,string.Empty),new Credentials(tokenHandler.WriteToken(token)));
        }
        return   new LoginResponse(new LoginResponseResult(false, "Wrong username or password"));
    }
}

public interface ILoginService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}

public record LoginRequest(string Email, string Password);
public record LoginResponse(LoginResponseResult Result, Credentials? Credentials = null);
public record Credentials(string Token);
public record LoginResponseResult(bool IsValid, string Message);