using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface IOrderService
{
    Order BuyProduct(PostOrderDTO dto);

    List<Order> getAllOrders();

    List<Order> GetAllOrdersByUser(int userId);
}