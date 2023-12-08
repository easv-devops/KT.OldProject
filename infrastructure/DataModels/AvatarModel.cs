using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class AvatarModel
{
    public int avatar_id { get; set; }
    [MinLength(3)] public string avatar_name { get; set; }
    [Range(1, Int32.MaxValue)] public int avatar_price { get; set;  }
    public string information { get; set;  }
    public string deleted { get; set; }
}