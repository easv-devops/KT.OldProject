namespace infrastructure.DataModels;

public class PasswordHashModel
{
    public int Id { get; set; }
    
    public required string Hash { get; set; }
    
    public required string Salt { get; set; }
    
}