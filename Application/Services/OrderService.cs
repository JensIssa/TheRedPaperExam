using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.VisualBasic.CompilerServices;

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
        ExceptionHandlingConstructor();
    }

    public Order CreateOrder(PostOrderDTO dto)
    {
        var validation = _postdto.Validate(dto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        dto.Products = _productrepository.GetProductsById(dto.ProductsId);
        _productrepository.SetProductsToSold(dto.Products);
        return _orderRepository.CreateOrder(_imapper.Map<Order>(dto));
    }

    public List<Order> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }

    public List<Order> GetAllOrdersByUser(int userId)
    {
        return _orderRepository.GetAllOrdersByUser(userId);
    }

    public List<Product> GetProductsByOrderId(int orderId)
    {
        return _productrepository.GetProductsByOrderId(orderId);
    }
    
    
    public void ExceptionHandlingConstructor()
    {
        if (_productrepository == null)
        {
            throw new ArgumentException("This service cannot be constructed without a product repository");
        }

        if (_imapper == null)
        {
            throw new ArgumentException("This service cannot be constructed without a mapper");
        }

        if (_orderRepository == null )
        {
            throw new ArgumentException("This service cannot be constructed without a order repository");
        }

        if (_postdto == null)
        {
            throw new ArgumentException("This service cannot be constructed without a postValidator");
        }
    }
}