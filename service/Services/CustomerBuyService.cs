using infrastructure.DataModels;
using infrastructure.Repositories;
namespace service.Services;

public class CustomerBuyService
{
    private readonly CustomerBuyRepository _customerBuyRepository;

    public CustomerBuyService(CustomerBuyRepository customerBuyRepository)
    {
        _customerBuyRepository = customerBuyRepository;
    }

    public IEnumerable<CustomerBuy> GetAllCustomerBuy()
    {
        return _customerBuyRepository.GetAllCustomerBuy();
    }

    public CustomerBuy CreateCustomerBuy(int order_id, int avatar_id)
    {
        return _customerBuyRepository.CreateCustomerBuy(order_id, avatar_id);
    }
    
    public void deleteCustomerBuy(int customer_buy_id)
    {
        _customerBuyRepository.DeleteCustomerBuy(customer_buy_id);
    }
}