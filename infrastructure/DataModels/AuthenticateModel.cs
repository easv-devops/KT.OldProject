namespace infrastructure.DataModels;

public class AuthenticateModel
{
    public string user_id { get; set; }
    
    public required string full_name { get; set; }
    
    public required string email { get; set; }
    
    public string? admin { get; set; }
}
