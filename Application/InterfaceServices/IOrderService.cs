using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface IOrderService
{
    Order CreateOrder(PostOrderDTO dto);

    List<Order> GetAllOrders();

    List<Order> GetAllOrdersByUser(int userId);
    
}