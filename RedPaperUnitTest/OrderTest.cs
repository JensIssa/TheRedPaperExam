using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace RedPaperUnitTest;

public class OrderTest
{
    private IMapper mapper;
    private OrderValidator postOrderValidator;

    public OrderTest()
    {
        var _mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostOrderDTO, Order>();
        }).CreateMapper();
        mapper = _mapper;
        postOrderValidator = new OrderValidator();
    }
    
    [Fact]
    public void CreateOrderServiceTest()
    {
        Mock<IOrderRepository> mockRepoOrder = new Mock<IOrderRepository>();
        Mock<IProductRepository> mockRepoProduct = new Mock<IProductRepository>();
        IOrderService service = new OrderService(mockRepoProduct.Object, mapper, mockRepoOrder.Object,postOrderValidator );
        // Assert
        Assert.NotNull(service);
        Assert.True(service is OrderService);
    }
    
    [Fact]
    public void CreateValidOrderTest()
    {
        List<Product> products = new List<Product>();
        Product product1 = new Product
        {
            Id = 1, ProductName = "TestProduct 1", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 5.5, ProductConditionId = 2 ,
            UserId = 1, SubCategoryID = 2, isSold = true};
        
        Product product2 = new Product
        {
            Id = 1, ProductName = "TestProduct 1", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 5.5, ProductConditionId = 2 ,
            UserId = 1, SubCategoryID = 2, isSold = true};
        
        products.Add(product1);
        products.Add(product2);

        Order order = new Order
        {
            userId = 2, Products = products
        };
        PostOrderDTO dto = new PostOrderDTO()
        {
            userId = order.userId, Products = order.Products
        };
        Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
        Mock<IOrderRepository> mockRepoOrder = new Mock<IOrderRepository>();
        IOrderService service =
            new OrderService(mockRepo.Object, mapper, mockRepoOrder.Object, postOrderValidator);
        
        mockRepoOrder.Setup(o => o.BuyProduct(It.IsAny<Order>())).Returns(order);
        
        var OrderCreated = service.BuyProduct(dto);
          
        //Assert
        Assert.Equal(order.Id, OrderCreated.Id);
        Assert.Equal(order.userId, OrderCreated.userId);
        Assert.Equal(order.Products, OrderCreated.Products);
        Assert.Equal(order, OrderCreated);
        mockRepoOrder.Verify(o => o.BuyProduct(It.IsAny<Order>()), Times.Once);
    }
}