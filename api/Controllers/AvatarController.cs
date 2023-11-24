using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class AvatarController : Controller
{
    private readonly AvatarService _avatarService;
    
    public AvatarController(AvatarService avatarService)
    {
        _avatarService = avatarService;
    }
    
    
    [HttpGet]
    [Route("/avatar/all")]
    public ResponseDto GetAllAvatars()
    {
        
        return new ResponseDto()
        {
            
            MessageToClient = "Succesfully got all Avatars",
            ResponseData =  _avatarService.GetAllAvatars()
        };
        
        
        
    }
    
    [HttpPost]
    [ValidateModel]
    [Route("/avatar")]
    public ResponseDto postAvatar([FromBody] Avatar avatar)
    {

        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            
            MessageToClient = "Succesfully created an Avatar",
            ResponseData = _avatarService.CreateAvatar(avatar.avatar_name, avatar.avatar_price, avatar.information)
        };
    }
        

    [HttpPut]
    [Route("/avatar/{avatar_id}")]
    public ResponseDto putAvatar([FromRoute] int avatar_id, [FromBody] Avatar avatar)
    {

        
        return new ResponseDto()
        {
            
            MessageToClient = "Succesfully updated an Avatar",
            ResponseData =  _avatarService.UpdateAvatar(avatar_id, avatar.avatar_name, avatar.avatar_price,avatar.information)
        };
        
        
        
    }

    
    [HttpDelete]
    [ValidateModel]
    [Route("/avatar/{avatar_id}")]
    public void deleteAvatar([FromRoute] int avatar_id)
    {

        HttpContext.Response.StatusCode = 204;
        new ResponseDto()
        {
            MessageToClient = "Succesfully deleted avatar"
           };
        _avatarService.deleteAvatar(avatar_id);
    }
}