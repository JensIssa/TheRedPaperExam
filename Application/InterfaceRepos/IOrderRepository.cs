using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IOrderRepository
{
    /// <summary>
    /// This method creates an order object to the database
    /// </summary>
    /// <param name="order">The order object</param>
    /// <returns>A new order object</returns>
    Order CreateOrder(Order order);

    /// <summary>
    /// This method returns a list of all the order objects from the database
    /// </summary>
    /// <returns>A list of all the order objects</returns>
    List<Order> GetAllOrders();

    /// <summary>
    /// This method returns a list of all the order objects connected to a specific user from the database 
    /// </summary>
    /// <param name="id">The id of the specific user object the order objects are connected to</param>
    /// <returns>a list of all the order objects connected to a specific user</returns>
    List<Order> GetAllOrdersByUser(int id);
}