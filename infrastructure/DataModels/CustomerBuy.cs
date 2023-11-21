using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class CustomerBuy
{
    public int id { get; set; }
    
    public int order_id { get; set; }
    
    public int avatar_id { get; set; }
    
    
    
}