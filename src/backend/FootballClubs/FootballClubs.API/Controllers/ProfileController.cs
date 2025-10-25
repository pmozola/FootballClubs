using FootballClubs.Profile.Application.Commands.CreateProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballClubs.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProfileController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add(CreateProfileCommand request)
    {
        await sender.Send(request);

        return Ok();
    }
}