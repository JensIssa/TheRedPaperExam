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
    private IMapper mapper;
    private ProductValidator.PostProductValidator postProductValidator;
    private ProductValidator.PutProductValidator putProductValidator;

    public ProductTest()
    {
        var _mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostProductDTO, Product>();
            config.CreateMap<PutProductDTO, Product>();
        }).CreateMapper();
        mapper = _mapper;
        postProductValidator = new ProductValidator.PostProductValidator();
        putProductValidator = new ProductValidator.PutProductValidator();
    }


    public static IEnumerable<Object[]> GetAllProductsFromSubcategoryTestcases()
    {
        Product product1 = new Product
        {
            Id = 1, ProductName = "TestProduct 1", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 5.5, ProductConditionId = 1,
             UserId = 1 };
         
        Product product2 = new Product
        {
            Id = 2, ProductName ="TestProduct 2", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 7.5, ProductConditionId = 2,
            UserId = 2 }; 
            
        Product product3 = new Product
        {
            Id = 3, ProductName = "TestProduct 3", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 6.5, ProductConditionId = 3,
             UserId = 3 };
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
            Description = "This is a description", Price = 5.5, ProductConditionId = 2,
            SubCategoryID = 1 };
        
        Product product2 = new Product
        {
            Id = 2, ProductName = "TestProduct 2", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 7.5, ProductConditionId = 3,
            SubCategoryID = 2 }; 
            
        Product product3 = new Product
        {
            Id = 3, ProductName = "TestProduct 3", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 6.5, ProductConditionId = 2,
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
        IProductService service = new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
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
        IProductService service =
            new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
        mockRepo.Setup(p => p.GetAllProductsFromSubcategory(subcategoryID)).Returns(fakeRepo.ToList);
        var actualResult = service.GetAllProductsFromSubcategory(2);
        Assert.Equal(expectedResult, actualResult);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actualResult));
        mockRepo.Verify(p => p.GetAllProductsFromSubcategory(2), Times.Once);
    }
    
        [Theory]
        [MemberData(nameof(GetAllProductsFromUserTestcases))]
        public void GetAllProductsFromUserTest(Product[] data, List<Product> expectedResult)
        {
          int userId = 2; 
          var fakeRepo = data;
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
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
              Description = "This is a description", Price = 5.5, ProductConditionId = 2 ,
              UserId = userId, SubCategoryID = subcategoryId};
          PostProductDTO dto = new PostProductDTO()
          {
              ProductName = product1.ProductName, Description = product1.Description, Price = product1.Price,
              UserId = product1.UserId, ProductConditionId = product1.ProductConditionId, ImageUrl = product1.ImageUrl,
              SubCategoryID = product1.SubCategoryID
          };
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
          IProductService service =
              new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
          mockRepo.Setup(p => p.AddProductToUser(It.IsAny<Product>())).Returns(product1);
          var productCreated = service.AddProductToUser(dto);
          
          //Assert
          Assert.Equal(product1.Id, productCreated.Id);
          Assert.Equal(product1.UserId, productCreated.UserId);
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
          IProductService service =
              new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
          Product product = new Product{ Id = productiD, ProductName = productName, ImageUrl = imageUrl,
              Description = description, Price = price, SubCategoryID = subId, UserId = userID, ProductConditionId = 2 };
          PostProductDTO dto = new PostProductDTO
          {
              Description = product.Description, ProductName = product.ProductName, Price = product.Price,
              ImageUrl = product.ImageUrl, UserId = product.UserId, SubCategoryID = product.SubCategoryID,
              ProductConditionId = product.ProductConditionId
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
              Description = "This is a description", Price = 5.5, ProductConditionId = 2,
              SubCategoryID = 1, UserId = 2};
        
          Product productToDelete = new Product
          {
              Id = 2, ProductName = "TestProduct 2", ImageUrl = "This is a tester",
              Description = "This is a description", Price = 7.5, ProductConditionId = 3,
              SubCategoryID = 1, UserId = 2}; 
          products.Add(product1);
          products.Add(productToDelete);
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
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

      [Theory]
      [InlineData(0, "The Product id is not found")]
      [InlineData(null, "The Product id is not found")]
      public void DeleteProductTestInvalid(int productID, string expectedException)
      {
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
          IProductService service =
              new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
          var action = () => service.DeleteProductFromUser(productID);
          var ex = Assert.Throws<ArgumentException>(action);
          Assert.Equal(expectedException, ex.Message);
      }
      
      [Theory]
      [InlineData(1, "Product test", "billede", "beskrivelse", 5000, 1, 2, 2)]
      public void updateProductTest(int productiD, string productName, string imageUrl, string description,
          double price, int subId, int userID, int productConditionId)
      {
          Product product3 = new Product
          {
              Id = 3, ProductName = "TestProduct 3", ImageUrl = "This is a tester",
              Description = "This is a description", Price = 6.5, ProductConditionId = 2,
              SubCategoryID = 3 };
          PutProductDTO dto = new PutProductDTO()
          {
              productId = product3.Id, ProductName = product3.ProductName, Description = product3.Description,
              Price = product3.Price, ImageUrl = product3.ImageUrl, ProductConditionId = product3.ProductConditionId
          };
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
          IProductService service =
              new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
          mockRepo.Setup(p => p.UpdateProduct(productiD, It.IsAny<Product>())).Returns(product3);

          dto.productId = productiD;
          dto.ProductName = productName;
          dto.Description = description;
          dto.ImageUrl = imageUrl;
          dto.Price = price;
          dto.ProductConditionId = productConditionId;
          var updatedProduct = service.UpdateProduct(productiD, dto);
          Assert.Equal(product3, updatedProduct);
          mockRepo.Verify(r => r.UpdateProduct(productiD, It.IsAny<Product>()), Times.Once);
      }

      [Theory]
      [InlineData(1, "", "testImageUrl", "testDescription", 2.5, 1,  "The productName is empty or null")]
      [InlineData(1, null, "testImageUrl", "testDescription", 2.5, 1, "The productName is empty or null")]
      [InlineData(1, "testProductName", "", "testDescription", 2.5, 1, "The imageUrl is empty or null")]
      [InlineData(1, "testProductName", null, "testDescription", 2.5, 1, "The imageUrl is empty or null")]
      [InlineData(1, "testProductName", "testImageU", "", 2.5, 1, "The description is empty or null")]
      [InlineData(1, "testProductName", "testImageU", null, 2.5, 1, "The description is empty or null")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", null, 1, "The price is null / <1")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", 0, 1, "The price is null / <1")]
      [InlineData(null, "testProductName", "testImageU", "testDescription", 2.5, 1,  "The productId is null / <1")]
      [InlineData(0, "testProductName", "testImageU", "testDescription", 2.5, 1,  "The productId is null / <1")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", 2.5, null,  "The conditionId is null / <1")]
      [InlineData(1, "testProductName", "testImageU", "testDescription", 2.5, 0,  "The conditionId is null / <1")]
      public void UpdateProductInvalidTest(int id,string productName, string imageUrl, string description, double price,int productConditionId, string expectedMessage)
      {
          PutProductDTO dto = new PutProductDTO()
          {
              productId = id, ProductName = productName, Description = description, Price = price, ImageUrl = imageUrl,
              ProductConditionId = productConditionId
          };
          Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
          IProductService service =
              new ProductService(mockRepo.Object, mapper, postProductValidator, putProductValidator);
          var action = () => service.UpdateProduct(id, dto);

          var ex = Assert.Throws<ArgumentException>(action);
          
          Assert.Equal(expectedMessage, ex.Message);
      }
}
