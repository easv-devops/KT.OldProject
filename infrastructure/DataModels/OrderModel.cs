using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class OrderModel
{
    public int order_id { get; set; }
    public int user_id { get; set; }
    
}