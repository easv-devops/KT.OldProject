using System.Security.Claims;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class UserController : ControllerBase
{
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult AdminsEndpoint()
    {
        var currentUser = GetCurrentUser();
        return Ok($"Hi {currentUser.full_name} you are an {currentUser.admin}");
    }
    
    
    [Route("api/get")]
    [HttpGet]
    private AuthenticateModel GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new AuthenticateModel()
            {
                full_name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                admin = userClaims.FirstOrDefault(o=>  o.Type == ClaimTypes.Role)?.Value,
                user_id = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value
                
            };
        }
        return null;
    }

    
}