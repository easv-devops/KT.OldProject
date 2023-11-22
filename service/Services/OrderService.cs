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

    public IEnumerable<Order> GetAllOrder()
    {
        return _orderRepository.GetAllOrder();
    }

    public Order CreateOrder(int user_id)
    {
        return _orderRepository.CreateOrder(user_id);
    }

    public Order UpdateOrder(int id, int user_id)
    {
        return _orderRepository.UpdateOrder(id, user_id);
    }

    public void deleteOrder(int ordre_id)
    {
        _orderRepository.DeleteOrder(ordre_id);
    }
}