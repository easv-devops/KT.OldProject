using System.ComponentModel.DataAnnotations;
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
        try
        {
            return _customerBuyRepository.GetAllCustomerBuy();
        }
        catch (Exception e)
        {
            throw new ValidationException("Error in getting all products attached to orders");
        }
       
    }

    /*
     * Creates a new customer buy record.
     */
    public CustomerBuyModel CreateCustomerBuy(CustomerBuyModel customerBuy)
    {
        
         try
                {
                    return _customerBuyRepository.CreateCustomerBuy(customerBuy);
                }
                catch (Exception e)
                {
                    throw new ValidationException("Error in attaching an avatar to an order");
                }
        
    }
    
    /*
     * Deletes a customer buy record based on its ID.
     */
    public void deleteCustomerBuy(int customer_buy_id)
    
    {
    
        try
        {
            _customerBuyRepository.DeleteCustomerBuy(customer_buy_id);
        }
        catch (Exception e)
        {
            throw new ValidationException("Error in deleting an avatar attached to an order");
        }
        
        
       
    }
}