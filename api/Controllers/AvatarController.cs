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
    public IEnumerable<Avatar> GetAllAvatars()
    {
        try
        {
            return _avatarService.GetAllAvatars();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error when getting all Avatars", e);
        }
    }
    
    [HttpPost]
    [Route("/avatar")]
    public object postAvatar([FromBody] Avatar avatar)
    {

        return _avatarService.CreateAvatar(avatar.avatar_name, avatar.price);
    }
        

    [HttpPut]
    [Route("/avatar/{avatar_id}")]
    public Object putAvatar([FromRoute] int avatar_id, [FromBody] Avatar avatar)
    {

        return _avatarService.UpdateAvatar(avatar_id, avatar.avatar_name, avatar.price);
    }

    
    [HttpDelete]
    [Route("/avatar/{avatar_id}")]
    public void deleteAvatar([FromRoute] int avatar_id)
    {
       
            _avatarService.deleteAvatar(avatar_id);
        }
    
        
  
    
    
}