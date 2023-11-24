using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service.Services;

public class AvatarService
{
    private readonly AvatarRepository _avatarRepository;
    
    public AvatarService(AvatarRepository avatarRepository)
    {
        _avatarRepository = avatarRepository;
    }
    
    
    public IEnumerable<Avatar> GetAllAvatars()
    {
        try
        {
            return _avatarRepository.GetAllAvatars();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in getting all  Avatars");
        }
        
        
       
    }
    
    
    public Avatar CreateAvatar(string avatar_name, int price, string information)
    {

        if (!ReferenceEquals(_avatarRepository.CheckIfNameExist(avatar_name), null))
            throw new ValidationException("Already exists");
        
        try
            {
                return _avatarRepository.CreateAvatar(avatar_name, price, information);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ValidationException("Error in creating an Avatar");
            }
        }
        
    
    
    public Avatar UpdateAvatar(int avatar_id, string avatar_name, int price,string information)
    {
      
        try
        {
            return _avatarRepository.UpdateAvatar(avatar_id, avatar_name, price,information);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in updating an Avatar");
        }
        
        
    }
    
    public void deleteAvatar(int avatar_id)
    {
        try
        {
             _avatarRepository.DeleteAvatar(avatar_id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in deleting an Avatar");
        }
        
        
        
        
        
        
    }
    
    
    
    
}
