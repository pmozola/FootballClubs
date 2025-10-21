using FootballClubs.Profile.Application.Commands.CreateProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballClubs.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add(CreateProfileCommand request)
    {
        await sender.Send(request);

        return Ok();
    }
}