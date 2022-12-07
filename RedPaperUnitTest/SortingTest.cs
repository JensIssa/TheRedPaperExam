using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Domain.Entities;
using Moq;

namespace RedPaperUnitTest;

public class SortingTest
{

    #region sortingListAlphabeticallyTestCasesBothCorrectAndReverse
    public static IEnumerable<Object[]> SortingAlphabeticallyTestCase()
    {
        Product product1 = new Product
        {
            Id = 1, ProductName = "A", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 5.5, ProductConditionId = 1,
            UserId = 1 };
         
        Product product2 = new Product
        {
            Id = 2, ProductName ="C", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 7.5, ProductConditionId = 2,
            UserId = 2 }; 
            
        Product product3 = new Product
        {
            Id = 3, ProductName = "B", ImageUrl = "This is a tester",
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
            new List<Product>() { product1, product3, product2 }
        };
    }
    public static IEnumerable<Object[]> SortingAlphabeticallyTestCaseReverse()
    {
        Product product1 = new Product
        {
            Id = 1, ProductName = "A", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 5.5, ProductConditionId = 1,
            UserId = 1 };
         
        Product product2 = new Product
        {
            Id = 2, ProductName ="C", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 7.5, ProductConditionId = 2,
            UserId = 2 }; 
            
        Product product3 = new Product
        {
            Id = 3, ProductName = "B", ImageUrl = "This is a tester",
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
            new List<Product>() { product2, product3, product1 }
        };
    }
    
    public static IEnumerable<Object[]> SortingAlphabeticallyTestCaseBySudId()
    {
        Product product1 = new Product
        {
            Id = 1, ProductName = "A", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 5.5, ProductConditionId = 1,
            UserId = 1, SubCategoryID = 2};
         
        Product product2 = new Product
        {
            Id = 2, ProductName ="C", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 7.5, ProductConditionId = 2,
            UserId = 2, SubCategoryID = 2}; 
            
        Product product3 = new Product
        {
            Id = 3, ProductName = "B", ImageUrl = "This is a tester",
            Description = "This is a description", Price = 6.5, ProductConditionId = 3,
            UserId = 3, SubCategoryID = 2};
        yield return new object[]
        {
            new Product[]
            {
                product1,
                product2,
                product3
            },
            new List<Product>() { product2, product3, product1 }
        };
    }
    
    #endregion

    [Fact]
    public void CreateSortingRepository()
    {
        Mock<ISortingRepository> sortingRepo = new Mock<ISortingRepository>();
        ISortingService service = new SortingService(sortingRepo.Object);
        Assert.NotNull(service);
        Assert.True(service is SortingService);
    }

    [Theory]
    [MemberData(nameof(SortingAlphabeticallyTestCase))]
    public void GetProductListAlphabeticallyValidInput(Product[] products, List<Product> expectedResult)
    {
        var fakeList = products;
        Mock<ISortingRepository> sortingRepo = new Mock<ISortingRepository>();
        ISortingService service = new SortingService(sortingRepo.Object);
        sortingRepo.Setup(r => r.SortProductsAlphabetically()).Returns(fakeList.ToList);
        var result = service.SortProductsAlphabetically();
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [MemberData(nameof(SortingAlphabeticallyTestCaseReverse))]
    public void GetProductListAlphabeticallyReverseValidInput(Product[] products, List<Product> expectedResult)
    {
        var fakeList = products;
        Mock<ISortingRepository> sortingRepo = new Mock<ISortingRepository>();
        ISortingService service = new SortingService(sortingRepo.Object);
        sortingRepo.Setup(r => r.SortProductsAlphabeticallyReverse()).Returns(fakeList.ToList);
        var result = service.SortProductsAlphabeticallyReverse();
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [MemberData(nameof(SortingAlphabeticallyTestCaseBySudId))]
    public void GetProductListAlphabeticallySubIdValidInput(Product[] products, List<Product> expectedResult)
    {
        var fakeList = products;
        Mock<ISortingRepository> sortingRepo = new Mock<ISortingRepository>();
        ISortingService service = new SortingService(sortingRepo.Object);
        sortingRepo.Setup(r => r.SortProductsAlphabeticallyBySubId(2)).Returns(fakeList.ToList);
        var result = service.SortProductsAlphabeticallyBySubId(2);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(null, "There is no Subcategory to view products from")]
    [InlineData(-2, "There is no Subcategory to view products from")]
    public void GetProductistAlphabeticallyBySubIdInvalidInput(int subId, string expectedException)
    {
        Mock<ISortingRepository> sortingRepo = new Mock<ISortingRepository>();
        ISortingService service = new SortingService(sortingRepo.Object);
        var action = () => service.SortProductsAlphabeticallyBySubId(subId);
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedException, ex.Message);
    }
    

}
    