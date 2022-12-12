using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IOrderRepository
{
    Order CreateOrder(Order order);

    List<Order> GetAllOrders();

    List<Order> GetAllOrdersByUser(int id);
}