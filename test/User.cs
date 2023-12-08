
using System.ComponentModel.DataAnnotations;

namespace test;


public class User
{
    public int user_id { get; set; }
    public string full_name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    
}

public class Avatar
{
    public int avatar_id { get; set; }
    public string avatar_name { get; set; }
    public int avatar_price { get; set; }
    public string information { get; set; }
    public  bool deleted { get; set; }
}
public class ResponseDto<T>
{
    public string MessageToClient { get; set; }
    public T? ResponseData { get; set; }
}

public class Order
{
    public int order_id { get; set; }
    public int user_id { get; set; }
}