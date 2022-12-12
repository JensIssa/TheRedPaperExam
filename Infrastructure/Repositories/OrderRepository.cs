using Application.InterfaceRepos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private  RepositoryDBContext _context;

    public OrderRepository(RepositoryDBContext context)
    {
        _context = context;
    }

    public Order CreateOrder(Order order)
    {
        _context.OrderTable.Add(order);
        _context.SaveChanges();
        return order;
    }

    public List<Order> GetAllOrders()
    {
        return _context.OrderTable.Include(o => o.Products).ToList();
    }

    public List<Order> GetAllOrdersByUser(int id)
    {
        return _context.OrderTable.Where(o => o.userId == id).Include(o => o.Products).ToList();
    }
}