using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace FootballClubs.Auth;

public interface IRegisterService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
}

public class RegisterService(UserManager<IdentityUser> userManager): IRegisterService
{
    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new IdentityUser{Email = request.Email, UserName = request.Email};
        var result = await userManager.CreateAsync(user, request.Password);
        return new RegisterResponse(result.Succeeded, result.Errors.Select(x => x.Description).ToArray());
    }
}

public record RegisterResponse(bool IsValid, string[] Errors);