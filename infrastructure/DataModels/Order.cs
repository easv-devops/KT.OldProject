using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class Order
{
    public int id { get; set; }
    public int user_id { get; set; }
}