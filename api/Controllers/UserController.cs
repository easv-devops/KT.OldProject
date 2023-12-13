using System.Security.Claims;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class UserController : ControllerBase
{
    
    /*
     * endpoint for admin users.
     * It requires the user to have the "Admin" role.
     * It returns an OK response with a personalized message that includes
     * the current user's full name and admin status. 
     */
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult AdminsEndpoint()
    {
        var currentUser = GetCurrentUser();
        return Ok($"Hi {currentUser.full_name} you are an {currentUser.admin}");
    }
    
    /*
     * retrieves the current authenticated user's information.
     * It extracts the user's full name, email, admin status, and user ID
     * from the claims in the user's identity. It returns an AuthenticateModel
     * object containing the user's information.
     */
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