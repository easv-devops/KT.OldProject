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
    public IEnumerable<Order> GetAllOrder()
    {
        try
        {
            return _orderService.GetAllOrder();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error when getting all Ordre", e);
        }
    }

    [HttpPost]
    [Route("/order")]
    public object postOrder([FromBody] Order order)
    {
        return _orderService.CreateOrder(order.user_id);
    }

    [HttpPut]
    [Route("/order/{id}")]
    public Object putOrder([FromBody] int id, [FromBody] Order order)
    {
        return _orderService.UpdateOrder(id, order.user_id);
    }

    [HttpDelete]
    [Route("/order/{id}")]
    public void deleteOrder([FromRoute] int id)
    {
        _orderService.deleteOrder(id);
    }
}