﻿using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public IEnumerable<OrderModel> GetAllOrder()
    {
        return _orderRepository.GetAllOrder();
    }

    public OrderModel CreateOrder(int user_id)
    {
        return _orderRepository.CreateOrder(user_id);
    }
    
    public void deleteOrder(int ordre_id)
    {
        _orderRepository.DeleteOrder(ordre_id);
    }
}