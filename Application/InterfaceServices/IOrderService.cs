using Application.DTOs;
using Domain.Entities;

namespace Application.InterfaceServices;

public interface IOrderService
{
    
    /// <summary>
    /// This method creates an order
    /// </summary>
    /// <param name="dto">This dto contains the properties used to create an order</param>
    /// <returns>returns a new order object</returns>
    Order CreateOrder(PostOrderDTO dto);
    /// <summary>
    /// This method returns a list of all the order objects
    /// </summary>
    /// <returns>returns a list of all the order objects</returns>
    List<Order> GetAllOrders();

    /// <summary>
    /// This method returns a list of all the order objects owned by a specific user
    /// </summary>
    /// <param name="userId">The userId of the specific user owning the order</param>
    /// <returns>A list of all the orders owned by the specific user</returns>
    List<Order> GetAllOrdersByUser(int userId);
    
}