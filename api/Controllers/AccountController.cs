using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service;
using service.Services;

namespace api.Controllers;

[ValidateModel]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly JwtService _jwtService;

    public AccountController(AccountService accountService, JwtService jwtService)
    {
        _accountService = accountService;
        _jwtService = jwtService;
        
    }

    [HttpPost]
    [Route("/api/account/login")]
    public ResponseDto Login([FromBody] LoginCommandModel model)
    {
        var user = _accountService.Authenticate(model);
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
            ResponseData =_accountService.Register(model)
        };
    }

    [HttpGet]
    [Route("api/account/getinfo")]
    public User GetInfo()
    {
        //Hardcoded til account 2?
        return _accountService.GetAccountInfo();
    }
}