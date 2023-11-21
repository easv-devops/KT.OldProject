using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class Ordre
{
    public int ordre_id { get; set; }
    public int user_id { get; set; }
}