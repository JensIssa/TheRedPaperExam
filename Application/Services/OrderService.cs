using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IProductRepository _productrepository;
    private readonly IMapper _imapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<PostOrderDTO> _postdto;

    public OrderService(IProductRepository productrepository, IMapper imapper, IOrderRepository orderRepository, IValidator<PostOrderDTO> postdto)
    {
        _productrepository = productrepository;
        _imapper = imapper;
        _orderRepository = orderRepository;
        _postdto = postdto;
    }

    public Order BuyProduct(PostOrderDTO dto)
    {
        var validation = _postdto.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        _productrepository.SetProductsToSold(dto.Products);
        return _orderRepository.BuyProduct(_imapper.Map<Order>(dto));
    }

    public List<Order> getAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }

    public List<Order> GetAllOrdersByUser(int userId)
    {
        return _orderRepository.GetAllOrdersByUser(userId);
    }
}