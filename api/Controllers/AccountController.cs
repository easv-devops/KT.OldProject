using System.Security.Authentication;
using api.Filters;
using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service;
using service.Models.Command;
using service.Services;

namespace api.Controllers;

[ValidateModel]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;
    private readonly JwtService _jwtService;

    public AccountController(AccountService service, JwtService jwtService)
    {
        _service = service;
        _jwtService = jwtService;
    }

    [HttpPost]
    [Route("/api/account/login")]
    public IActionResult Login([FromBody] LoginCommandModel model)
    {
        var user = _service.Authenticate(model);
        if (user == null) return Unauthorized();
        var token = _jwtService.IssueToken(SessionData.FromUser(user!));
        return Ok(new { token });
    }

    [HttpPost]
    [Route("/api/account/register")]
    public object Register([FromBody] RegisterCommandModel model)
    {
      return _service.Register(model);
    }
}