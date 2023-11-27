using System.Security.Authentication;
using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
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
    public ResponseDto Login([FromBody] LoginCommandModel model)
    {
        var user = _service.Authenticate(model);



        if (ReferenceEquals(user, null))
            return new ResponseDto()
            {
                MessageToClient = "Unauthorized ",
                ResponseData =Unauthorized() 
            };
            
        
        var token = _jwtService.IssueToken(SessionData.FromUser(user!));
        
        
        return new ResponseDto()
        {
            MessageToClient = "Welcome "+ user.full_name,
            ResponseData =new { token }
        };
        
        
        
    }

    [HttpPost]
    [Route("/api/account/register")]
    public ResponseDto Register([FromBody] RegisterCommandModel model)
    {
        return new ResponseDto()
        {
            MessageToClient = "Register new user",
            ResponseData =_service.Register(model)
        };
    }

    [HttpGet]
    [Route("api/account/getinfo")]

    public User GetInfo()
    {
        return _service.GetAccountInfo();
    }
    
}