using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class Avatar
{
    public int Id { get; set; }
    [MinLength(3)]
    public string Name { get; set;  }
    public int Price { get; set;  }
}