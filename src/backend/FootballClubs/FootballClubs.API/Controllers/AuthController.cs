using FootballClubs.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = FootballClubs.Auth.LoginRequest;


namespace FootballClubs.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IRegisterService registerService, ILoginService loginService) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await registerService.RegisterAsync(request);

        return result.IsValid ? Ok() : BadRequest(result.Errors);
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await loginService.LoginAsync(request);

        return result.Result.IsValid ? Ok(result.Credentials!.Token) : BadRequest(result.Result.Message);
    }
}