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
        return _avatarRepository.GetAllAvatars();
    }
    
    
    public Avatar CreateAvatar(string avatar_name, int price)
    {
        return _avatarRepository.CreateAvatar(avatar_name, price);
    }

    
    public Avatar UpdateAvatar(int avatar_id, string avatar_name, int price)
    {
        return _avatarRepository.UpdateAvatar(avatar_id, avatar_name, price);
    }
    
    public void deleteAvatar(int avatar_id)
    {
        _avatarRepository.DeleteAvatar(avatar_id);
    }
    
    
    
    
    
    
    
    
    
}
