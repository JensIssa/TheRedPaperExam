﻿using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
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
    
    [Theory]
    [InlineData("This service cannot be constructed without a product repository")]
    public void CreateInvalidOrderServiceWithoutProductRepository(string expectedMessage)
    {
        Mock<IOrderRepository> mockRepoOrder = new Mock<IOrderRepository>();
        var action = () => new OrderService(null, mapper, mockRepoOrder.Object, postOrderValidator);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    
    [Theory]
    [InlineData("This service cannot be constructed without a mapper")]
    public void CreateInvalidOrderServiceWithoutMapper(string expectedMessage)
    {
        Mock<IProductRepository> mockRepoProduct = new Mock<IProductRepository>();
        Mock<IOrderRepository> mockRepoOrder = new Mock<IOrderRepository>();
        var action = () => new OrderService(mockRepoProduct.Object, null, mockRepoOrder.Object, postOrderValidator);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    
    [Theory]
    [InlineData("This service cannot be constructed without a order repository")]
    public void CreateInvalidOrderServiceWithoutOrderRepository(string expectedMessage)
    {
        Mock<IProductRepository> mockRepoProduct = new Mock<IProductRepository>();
        var action = () => new OrderService(mockRepoProduct.Object, mapper, null, postOrderValidator);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    
    [Theory]
    [InlineData("This service cannot be constructed without a postValidator")]
    public void CreateInvalidOrderServiceWithoutPostValidator(string expectedMessage)
    {
        Mock<IOrderRepository> mockRepoOrder = new Mock<IOrderRepository>();
        Mock<IProductRepository> mockRepoProduct = new Mock<IProductRepository>();
        var action = () => new OrderService(mockRepoProduct.Object, mapper, mockRepoOrder.Object, null);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
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

        List<int> productIds = new List<int>();
        productIds.Add(1);
        productIds.Add(1);

        Order order = new Order
        {
            UserId = 2, Products = products
        };
        PostOrderDTO dto = new PostOrderDTO()
        {
            UserId = order.UserId, Products = order.Products, ProductsId = productIds
        };
        Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
        Mock<IOrderRepository> mockRepoOrder = new Mock<IOrderRepository>();
        IOrderService service =
            new OrderService(mockRepo.Object, mapper, mockRepoOrder.Object, postOrderValidator);
        
        mockRepoOrder.Setup(o => o.CreateOrder(It.IsAny<Order>())).Returns(order);
        
        var OrderCreated = service.CreateOrder(dto);
          
        //Assert
        Assert.Equal(order.Id, OrderCreated.Id);
        Assert.Equal(order.UserId, OrderCreated.UserId);
        Assert.Equal(order.Products, OrderCreated.Products);
        Assert.Equal(order, OrderCreated);
        mockRepoOrder.Verify(o => o.CreateOrder(It.IsAny<Order>()), Times.Once);
    }

    [Theory]
    [InlineData(0, typeof(ValidationException))]
    [InlineData(null, typeof(ValidationException))]
    public void InvalidCreateOrderTest(int userId, Type exceptionMessage)
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

        List<int> productIds = new List<int>();
        productIds.Add(1);
        
        Order order = new Order
        {
            UserId = userId, Products = products
        };

        PostOrderDTO dto = new PostOrderDTO()
        {
            UserId = order.UserId,
            Products = order.Products
        };
        
        
        Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
        Mock<IOrderRepository> mockRepoOrder = new Mock<IOrderRepository>();
        IOrderService service =
            new OrderService(mockRepo.Object, mapper, mockRepoOrder.Object, postOrderValidator);
        
        var ex = Assert.Throws<ValidationException>(() => service.CreateOrder(dto));
        
        Assert.Equal(exceptionMessage, ex.GetType());
    }
    
}