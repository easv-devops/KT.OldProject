using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    
    public OrderService(OrderRepository orderRepository)
    {
       
        _orderRepository = orderRepository;
    }

    /*
     * Retrieves all orders from the repository.
     */
    public IEnumerable<OrderModel> GetAllOrder()
    {
     
        try
        {
           return _orderRepository.GetAllOrder();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error getting all orders");
        }
        
    }

    /*
     * Creates a new order for the specified user.
     */
    public OrderModel CreateOrder(int user_id)
    {
        
        try
        {
            return _orderRepository.CreateOrder(user_id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error creating an order");
        }
      
        
    }
    
    /*
     * Deletes an order based on the provided order ID.
     */
    public void deleteOrder(int ordre_id)
    {
        
        try
        {
            _orderRepository.DeleteOrder(ordre_id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error deleting an order");
        }
        
        
       
       
    }
    
    /*
     * Creates a customer purchase with specified user ID and avatar models.
     */
    public void CreateCustomerBuy(int user_id, AvatarModel[] avatars)
    {
        
        try
        {
            _orderRepository.CreateCustomerBuy(user_id, avatars);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error creating attaching Avatars to an order");
        }
        
      
    }

    /*
     * Retrieves the last order for a user to send via email.
     */
    public OrderModel getLastOrderToEmail(int user_id)
    {
        
        try
        {
            return _orderRepository.getLastOrderToEmailOrPDF(user_id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in getting order to email or pdf");
        }
        
        
    }
    
}