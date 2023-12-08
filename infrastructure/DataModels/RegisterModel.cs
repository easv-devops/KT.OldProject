using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class RegisterModel
{
    
    [Required] [MinLength(2)] public required string full_name { get; set; }
   
    [Required] [MinLength(9)] public required string email { get; set; }
   
    [Required] [MinLength(8)] public required string password { get; set; }
}