using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class LoginController : ControllerBase
{
    private LoginService _service;

    public LoginController(LoginService service)
    {
        _service = service;
       
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("/api/account/login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
   
        var user = _service.Authenticate(model);

        if (model != null)
        {
            var token = _service.Generate(user);
            return Ok(token);
        }

        return NotFound("User not found");

    }
    
    [HttpPost]
    [Route("/api/account/register")]
    public ResponseDto Register([FromBody] RegisterModel model)
    {
        return new ResponseDto()
        {
            MessageToClient = "Register new user",
            ResponseData =_service.Register(model)
        };
    }
}