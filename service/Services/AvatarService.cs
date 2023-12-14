using System.ComponentModel.DataAnnotations;
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
    
    /*
     * Retrieves all avatars.
     */
    public IEnumerable<AvatarModel> GetAllAvatars()
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
    
    public IEnumerable<AvatarModel> GetAllDeletedAvatars()
    {
        try
        {
            return _avatarRepository.GetAllDeletedAvatars();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in getting all deleted Avatars");
        }
    }
    
    /*
     * Creates a new avatar.
     */
    public AvatarModel CreateAvatar(AvatarModel avatar)
    {

        if (!ReferenceEquals(_avatarRepository.CheckIfNameExist(avatar.avatar_name), null))
            throw new ValidationException("Already exists");
        
        try {
                return _avatarRepository.CreateAvatar(avatar);
        }catch (Exception e) { throw new ValidationException("Error in creating an Avatar");
        }
    }
        
    
    /*
     * Updates an existing avatar.
     */
    public AvatarModel UpdateAvatar(int avatarId, AvatarModel avatar)
    {
        try
        {
            return _avatarRepository.UpdateAvatar(avatarId,avatar);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in updating an Avatar");
        }
    }
    
    /*
     * Deletes an avatar by its ID.
     */
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

    public void enableAvatar(int avatar_id)
    {
        try
        {
           _avatarRepository.enableAvatar(avatar_id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error while enabling an avatar");
        }
    }

    /*
     * Retrieves information about a specific avatar.
     */
    public string GetAvatarInformation(int avatar_id)
    {
        try
        {
            return _avatarRepository.GetAvatarInformation(avatar_id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in getting all avatar information");
        }
    }
}
