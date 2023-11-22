using api.Filters;
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
            MessageToClient = "Succesfully got ",
            ResponseData = _customerBuyService.GetAllCustomerBuy()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/customerbuy")]
    public ResponseDto postCustomerBuy([FromBody] CustomerBuy customerbuy)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Succesfully created an customerbuy ", 
            ResponseData = _customerBuyService.CreateCustomerBuy(customerbuy.order_id, customerbuy.avatar_id)
        };
      
    }
    
    [HttpDelete]
    [ValidateModel]
    [Route("/customerbuy/{customer_buy}")]
    public void deleteCustomerBuy([FromBody] int customer_buy_id)
    {
        HttpContext.Response.StatusCode = 204;
        new ResponseDto()
        {
            MessageToClient = "Successfully deleted customerbuy"
        };
        _customerBuyService.deleteCustomerBuy(customer_buy_id);
    }
}