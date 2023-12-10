using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class OrderModel
{
    public int user_id { get; set; }

    public AvatarModel[] avatarArray { get; set; }

}