using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class OrderController : Controller
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Route("/order/all")]
    public ResponseDto GetAllOrder()
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got all Order",
            ResponseData = _orderService.GetAllOrder()
        };
        
    }

    [HttpPost]
    [Route("/order")]
    public object postOrder([FromBody] Order order)
    {
        return _orderService.CreateOrder(order.user_id);
    }
    
    [HttpDelete]
    [Route("/order/{id}")]
    public void deleteOrder([FromRoute] int id)
    {
        _orderService.deleteOrder(id);
    }
}