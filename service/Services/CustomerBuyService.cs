using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service.Services;

public class CustomerBuyService
{
    private readonly CustomerBuyRepository _customerBuyRepository;

    /*
     * Constructor to initialize CustomerBuyService with a CustomerBuyRepository.
     */
    public CustomerBuyService(CustomerBuyRepository customerBuyRepository)
    {
        _customerBuyRepository = customerBuyRepository;
    }

    /*
     * Retrieves all customer buy records.
     */
    public IEnumerable<CustomerBuyModel> GetAllCustomerBuy()
    {
        return _customerBuyRepository.GetAllCustomerBuy();
    }

    /*
     * Creates a new customer buy record.
     */
    public CustomerBuyModel CreateCustomerBuy(CustomerBuyModel customerBuy)
    {
        return _customerBuyRepository.CreateCustomerBuy(customerBuy);
    }
    
    /*
     * Deletes a customer buy record based on its ID.
     */
    public void deleteCustomerBuy(int customer_buy_id)
    {
        _customerBuyRepository.DeleteCustomerBuy(customer_buy_id);
    }
}