using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace RedPaperUnitTest;

public class ProductTest
{
    public static IEnumerable<Object[]> GetAllProductsFromSubcategoryTestcases()
    {
        Product product1 = new Product
        {
            Id = 1, ProductName = "TestProduct 1", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 5.5, ProductCondition = Condition.Fremragende,
             userId = 1 };
         
        Product product2 = new Product
        {
            Id = 2, ProductName ="TestProduct 2", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 7.5, ProductCondition = Condition.Brugt,
            userId = 2 }; 
            
        Product product3 = new Product
        {
            Id = 3, ProductName = "TestProduct 3", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 6.5, ProductCondition = Condition.Nedslidt,
             userId = 3 };
        yield return new object[]
        {
            new Product[]
            {
                product1,
                product2,
                product3
            },
            new List<Product>() { product1, product2, product3 }
        };
    }
    
    public static IEnumerable<Object[]> GetAllProductsFromUserTestcases()
    {
        Product product1 = new Product
        {
            Id = 1, ProductName = "TestProduct 1", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 5.5, ProductCondition = Condition.Fremragende,
            SubCategoryID = 1 };
        
        Product product2 = new Product
        {
            Id = 2, ProductName = "TestProduct 2", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 7.5, ProductCondition = Condition.Brugt,
            SubCategoryID = 2 }; 
            
        Product product3 = new Product
        {
            Id = 3, ProductName = "TestProduct 3", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 6.5, ProductCondition = Condition.Nedslidt,
            SubCategoryID = 3 };
        yield return new object[]
        {
            new Product[]
            {
                product1,
                product2,
                product3
            },
            new List<Product>() { product1, product2, product3 }
        };
    }

    [Fact]
    public void CreateProductServiceTest()
    {
        Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostProductDTO, Product>();
            config.CreateMap<PutProductDTO, Product>();
        }).CreateMapper();
        var postProductValidator = new ProductValidator.PostProductValidator();
        var putProductValidator = new ProductValidator.PutProductValidator();
        IProductService service =
            new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
        // Assert
        Assert.NotNull(service);
        Assert.True(service is ProductService);
    }

    [Theory]
    [MemberData(nameof(GetAllProductsFromSubcategoryTestcases))]

    public void GetAllProductsFromSubcategoryTest(Product[] data, List<Product> expectedResult)
    {
        int subcategoryID = 2; 
        var fakeRepo = data;
        Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostProductDTO, Product>();
            config.CreateMap<PutProductDTO, Product>();
        }).CreateMapper();
        var postProductValidator = new ProductValidator.PostProductValidator();
        var putProductValidator = new ProductValidator.PutProductValidator();
        IProductService service =
            new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
        mockRepo.Setup(p => p.GetAllProductsFromSubcategory(subcategoryID)).Returns(fakeRepo.ToList);
        var actualResult = service.GetAllProductsFromSubcategory(2);
        Assert.Equal(expectedResult, actualResult);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actualResult));
        mockRepo.Verify(p => p.GetAllProductsFromSubcategory(2), Times.Once);
    }
    
        [Theory]
        [MemberData(nameof(GetAllProductsFromSubcategoryTestcases))]
  
      public void GetAllProductsFromUserTest(Product[] data, List<Product> expectedResult)
      {
          int userId = 2; 
          var fakeRepo = data;
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
          var mapper = new MapperConfiguration(config =>
          {
              config.CreateMap<PostProductDTO, Product>();
              config.CreateMap<PutProductDTO, Product>();
          }).CreateMapper();
          var postProductValidator = new ProductValidator.PostProductValidator();
          var putProductValidator = new ProductValidator.PutProductValidator();
          IProductService service =
              new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
          mockRepo.Setup(p => p.GetAllProductsFromUser(userId)).Returns(fakeRepo.ToList);
          var actualResult = service.GetAllProductsFromUser(2);
          Assert.Equal(expectedResult, actualResult);
          Assert.True(Enumerable.SequenceEqual(expectedResult, actualResult));
          mockRepo.Verify(p => p.GetAllProductsFromUser(2), Times.Once);
      }

      [Theory]
      [InlineData(1, 2)]
      public void CreateValidProductTest(int userId, int subcategoryId)
      {
          Product product1 = new Product
          {
              Id = 1, ProductName = "TestProduct 1", ImageUrl = "This is a tester",
              Description = "This is a description", Price = 5.5, ProductCondition = Condition.Fremragende,
              userId = userId, SubCategoryID = subcategoryId};
          PostProductDTO dto = new PostProductDTO()
          {
              ProductName = product1.ProductName, Description = product1.Description, Price = product1.Price,
              userId = product1.userId, ProductCondition = product1.ProductCondition, ImageUrl = product1.ImageUrl,
              SubCategoryID = product1.SubCategoryID
          };
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
          var mapper = new MapperConfiguration(config =>
          {
              config.CreateMap<PostProductDTO, Product>();
              config.CreateMap<PutProductDTO, Product>();
          }).CreateMapper();
          var postProductValidator = new ProductValidator.PostProductValidator();
          var putProductValidator = new ProductValidator.PutProductValidator();
          IProductService service =
              new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
          mockRepo.Setup(p => p.AddProductToUser(It.IsAny<Product>())).Returns(product1);
          var productCreated = service.AddProductToUser(dto);
          
          //Assert
          Assert.Equal(product1.Id, productCreated.Id);
          Assert.Equal(product1.userId, productCreated.userId);
          Assert.Equal(product1, productCreated);
          mockRepo.Verify(p => p.AddProductToUser(It.IsAny<Product>()), Times.Once);
      }

      [Theory]
      [InlineData(1, "", "testImageUrl", "testDescription", 2.5, 1, 2, "The productName is empty or null")]
      [InlineData(1, null, "testImageUrl", "testDescription", 2.5, 1, 2, "The productName is empty or null")]
      [InlineData(1, "testProductName", "", "testDescription", 2.5, 1, 2, "The imageUrl is empty or null")]
      [InlineData(1, "testProductName", null, "testDescription", 2.5, 1, 2, "The imageUrl is empty or null")]
      [InlineData(1, "testProductName", "testImageU", "", 2.5, 1, 2, "The description is empty or null")]
      [InlineData(1, "testProductName", "testImageU", null, 2.5, 1, 2, "The description is empty or null")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", null, 1, 2, "The price is null / <1")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", 0, 1, 2, "The price is null / <1")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", 2.5, 0, 2, "The subCategoryId is null / <1")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", 2.5, null, 2,
          "The subCategoryId is null / <1")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", 2.5, 1, null, "The userID is null / <1")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", 2.5, 1, 0, "The userID is null / <1")]
      public void CreateInvalidProductTest(int productiD, string productName, string imageUrl, string description,
          double price, int subId, int userID, string expectedMesssage)
      {
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
          var mapper = new MapperConfiguration(config =>
          {
              config.CreateMap<PostProductDTO, Product>();
              config.CreateMap<PutProductDTO, Product>();
          }).CreateMapper();
          var postProductValidator = new ProductValidator.PostProductValidator();
          var putProductValidator = new ProductValidator.PutProductValidator();
          IProductService service =
              new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);

          Product product = new Product{ Id = productiD, ProductName = productName, ImageUrl = imageUrl,
              Description = description, Price = price, SubCategoryID = subId, userId = userID, ProductCondition = Condition.Fremragende};
          PostProductDTO dto = new PostProductDTO
          {
              Description = product.Description, ProductName = product.ProductName, Price = product.Price,
              ImageUrl = product.ImageUrl, userId = product.userId, SubCategoryID = product.SubCategoryID,
              ProductCondition = product.ProductCondition
          };

          var action = () => service.AddProductToUser(dto);

          var ex = Assert.Throws<ArgumentException>(action);

          Assert.Equal(expectedMesssage, ex.Message);
          mockRepo.Verify(r => r.AddProductToUser(product), Times.Never);
      }

      [Theory]
      [InlineData(1)]
      public void DeleteProductTest(int expectedListSize)
      {
          List<Product> products = new List<Product>();
          Product product1 = new Product
          {
              Id = 1, ProductName = "TestProduct 1", ImageUrl = "This is a tester",
              Description = "This is a description", Price = 5.5, ProductCondition = Condition.Fremragende,
              SubCategoryID = 1, userId = 2};
        
          Product productToDelete = new Product
          {
              Id = 2, ProductName = "TestProduct 2", ImageUrl = "This is a tester",
              Description = "This is a description", Price = 7.5, ProductCondition = Condition.Brugt,
              SubCategoryID = 1, userId = 2}; 
          products.Add(product1);
          products.Add(productToDelete);
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
          var mapper = new MapperConfiguration(config =>
          {
              config.CreateMap<PostProductDTO, Product>();
              config.CreateMap<PutProductDTO, Product>();
          }).CreateMapper();
          var postProductValidator = new ProductValidator.PostProductValidator();
          var putProductValidator = new ProductValidator.PutProductValidator();
          IProductService service =
              new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
          mockRepo.Setup(p => p.GetAllProductsFromUser(2)).Returns(products);
          mockRepo.Setup(p => p.DeleteProductFromUser(productToDelete.Id)).Returns(() =>
          {
              products.Remove(productToDelete);
              return productToDelete;
          });
          var actual = service.DeleteProductFromUser(2);
          
          Assert.Equal(expectedListSize, products.Count);
          Assert.Equal(productToDelete, actual);
          Assert.DoesNotContain(productToDelete, products);
          mockRepo.Verify(p => p.DeleteProductFromUser(2), Times.Once);
      }
}
