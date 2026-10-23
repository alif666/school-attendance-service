using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Application.Welcome;

namespace SchoolAttendance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WelcomeController : ControllerBase
{
    private readonly IWelcomeService _welcomeService;

    public WelcomeController(IWelcomeService welcomeService)
    {
        _welcomeService = welcomeService;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok(_welcomeService.GetMessage());
    }
}