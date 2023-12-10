using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

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
            MessageToClient = "Succesfully got all Order",
            ResponseData = _orderService.GetAllOrder()
        };
    }

    
    [HttpDelete]
    //[ValidateModel]
    [Route("api/order/{order_id}")]
    public void deleteOrder([FromRoute] int order_id)
    {
        HttpContext.Response.StatusCode = 204;
        _orderService.DeleteOrder(order_id);
        new ResponseDto()
        {
            MessageToClient = "Succesfully deleted an order"
        };
    }
    
    
    [HttpPost]
    [ValidateModel]
    [Route("/api/orderWithProducts")]
    public void PostOrder([FromBody] OrderModel model)
    {
        
        Console.WriteLine(model.user_id);
        Console.WriteLine(model.avatarArray[0]);
        Console.WriteLine("HEJ MED DIG  JEG BLIVER KALDT");
        
        //HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        //_orderService.CreateCustomerBuy(model);
        //_orderService.CreateOrder(model.user_id);
        
        
        
        
       //OrderModel order1 = _orderService.getLastOrderToEmail(model.user_id);

      
       
       //_emailService.SendEmail(order1.order_id);
        
       
    }

    }