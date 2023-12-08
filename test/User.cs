
using System.ComponentModel.DataAnnotations;

namespace test;


public class UserModel
{
    
    public int user_id { get; set; }
    [Required] [MinLength(2)] public required string full_name { get; set; }
   
    [Required] [MinLength(7)] public required string email { get; set; }
   
    [Required] [MinLength(8)] public required string password { get; set; }
    
}

public class AvatarModel
{
    public int avatar_id { get; set; }
    public string avatar_name { get; set; }
    public int avatar_price { get; set; }
    public string information { get; set; }
    public  bool deleted { get; set; }
}
public class ResponseDto<T>
{
    public string MessageToClient { get; set; }
    public T? ResponseData { get; set; }
}

public class OrderModel
{
    public int order_id { get; set; }
    public int user_id { get; set; }
}