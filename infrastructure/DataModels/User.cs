using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class User
{
    public int Id { get; set; }
    public required string full_name { get; set; }
    
    public required string Street { get; set; }
    
    public required int Zip { get; set; }
    
    public required string Email { get; set; }
    public required bool IsAdmin { get; set; }
}