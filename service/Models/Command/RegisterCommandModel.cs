using System.ComponentModel.DataAnnotations;

namespace service.Models.Command;

public class RegisterCommandModel
{
    [Required] public required string full_name { get; set; }

    [Required] public required string Street { get; set; }
    
    [Required] public required int Zip { get; set; }
    
    [Required] public required string Email { get; set; }

    [Required] [MinLength(8)] public required string password { get; set; }

   
}