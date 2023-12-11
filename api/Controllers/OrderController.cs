using System.ComponentModel.DataAnnotations;
using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

[ApiController]
public class OrderController : Controller
{
    private readonly OrderService _orderService;
    private readonly EmailService _emailService;

    public OrderController(OrderService orderService, EmailService emailService)
    {
        _orderService = orderService;
        _emailService = emailService;
    }

    [HttpGet]
    [Route("api/order/all")]
    public ResponseDto GetAllOrder()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully got all Order",
            ResponseData = _orderService.GetAllOrder()
        };
    }

    [HttpPost]
    //[ValidateModel]
    [Route("api/order/post")]
    public ResponseDto postOrder([FromBody] OrderModel order)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created an order",
            ResponseData = _orderService.CreateOrder(order.user_id)
        };
    }
    
    [HttpDelete]
    //[ValidateModel]
    [Route("api/order/{order_id}")]
    public void deleteOrder([FromRoute] int order_id)
    {
        HttpContext.Response.StatusCode = 204;
        _orderService.deleteOrder(order_id);
        new ResponseDto()
        {
            MessageToClient = "Successfully deleted an order"
        };
    }
    
    
    [HttpPost]
   // [ValidateModel]
    [Route("/orderWithProducts")]
    public ResponseDto postOrder([FromBody] OrderWithProducts orderWithProducts)
    {
        Console.WriteLine("Hej med dig "+orderWithProducts.userId);
        //HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        _orderService.CreateCustomerBuy(orderWithProducts.userId, orderWithProducts.avatar);
        
       
       OrderModel order1 = _orderService.getLastOrderToEmail(orderWithProducts.userId);

      
       
       _emailService.SendEmail(order1.order_id);
        
        
        return new ResponseDto()
        {
            MessageToClient = "Successfully created an order"
             
        };
    }

    }
    
    

public class OrderWithProducts
{
    [Required] [Range(1, Int32.MaxValue)]
    public int userId { get; set; }
    public AvatarModel[] avatar { get; set; }
}
    