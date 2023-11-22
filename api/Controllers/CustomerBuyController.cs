using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;
namespace api.Controllers;

public class CustomerBuyController : Controller
{
    private readonly CustomerBuyService _customerBuyService;

    public CustomerBuyController(CustomerBuyService customerBuyService)
    {
        _customerBuyService = customerBuyService;
    }

    [HttpGet]
    [Route("/customerbuy/all")]
    public ResponseDto GetAllCustomerBuy()
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got "
        }
        try
        {
            return _customerBuyService.GetAllCustomerBuy();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error when getting all CustomerBuy", e);
        }
    }

    [HttpPost]
    [Route("/customerbuy")]
    public object postCustomerBuy([FromBody] CustomerBuy customerbuy)
    {
        return _customerBuyService.CreateCustomerBuy(customerbuy.order_id, customerbuy.avatar_id);
    }
    
    [HttpDelete]
    [Route("/customerbuy{customer_buy}")]
    public void deleteCustomerBuy([FromBody] int customer_buy_id)
    {
        _customerBuyService.deleteCustomerBuy(customer_buy_id);
    }
}