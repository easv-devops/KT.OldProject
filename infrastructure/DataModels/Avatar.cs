using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class Avatar
{
    public int avatar_id { get; set; }
    
    [MinLength(3)]
    public string avatar_name { get; set; }
    
    public int avatar_price { get; set;  }
}