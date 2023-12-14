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
        return _orderRepository.GetAllOrder();
    }

    /*
     * Creates a new order for the specified user.
     */
    public OrderModel CreateOrder(int user_id)
    {
        return _orderRepository.CreateOrder(user_id);
    }
    
    /*
     * Deletes an order based on the provided order ID.
     */
    public void deleteOrder(int ordre_id)
    {
        _orderRepository.DeleteOrder(ordre_id);
    }
    
    /*
     * Creates a customer purchase with specified user ID and avatar models.
     */
    public void CreateCustomerBuy(int user_id, AvatarModel[] avatars)
    {
         _orderRepository.CreateCustomerBuy(user_id, avatars);
    }

    /*
     * Retrieves the last order for a user to send via email.
     */
    public OrderModel getLastOrderToEmail(int user_id)
    {
        return _orderRepository.getLastOrderToEmail(user_id);
    }
    
}