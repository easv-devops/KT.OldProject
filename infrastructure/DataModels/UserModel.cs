
namespace infrastructure.DataModels;

public class UserModel
{
    public int user_id { get; set; }
    
    public required string full_name { get; set; }
    
    public required string email { get; set; }
    
    public string? admin { get; set; }
}