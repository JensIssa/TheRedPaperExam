using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IOrderRepository
{
    public Order CreateOrder(Order order);

    public List<Order> GetAllOrders();

    public List<Order> GetAllOrdersByUser(int id);
}