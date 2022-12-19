using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Moq;

namespace RedPaperUnitTest;

public class CategoryTest
{
    private IMapper mapper;
    private CategoryValidator.CategoryPostValidator postCategoryValidator;
    private CategoryValidator.CategoryPutValidator putCategoryValidator;

    /// <summary>
    /// Constructor, where we instalize our mapper and validators to be used in all test.
    /// </summary>
    public CategoryTest()
    {
        var _mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutCategoryDTO, Category>();
            config.CreateMap<PostCategoryDTO, Category>();
        }).CreateMapper();
        mapper = _mapper;
        postCategoryValidator = new CategoryValidator.CategoryPostValidator();
        putCategoryValidator = new CategoryValidator.CategoryPutValidator();
    }
    /// <summary>
    /// List of categories used in GetAllCategories() valid test
    /// </summary>
    /// <returns>returns an IEnumerable object list </returns>
    public static IEnumerable<Object[]> GetAllCategories_Test()
    {
        Category category1 = new Category { Id = 1, CategoryName = "TestCategory 1" };
        Category category2 = new Category { Id = 1, CategoryName = "TestCategory 2" };
        Category category3 = new Category { Id = 1, CategoryName = "TestCategory 3" };
        yield return new object[]
        {
            new Category[]
            {
                category1,
                category2,
                category3
            },
            new List<Category>() { category1, category2, category3 }
        };
    }

    /// <summary>
    /// Tests whether we can construct a valid CategoryService
    /// </summary>
    [Fact]
    public void CreateCategoryService()
    {
        //arrange
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        //Assert
        Assert.NotNull(service);
        Assert.True(service is CategoryService);
    }

    /// <summary>
    /// Tests whether we get an exception when we try to create a Category service without a repository
    /// </summary>
    /// <param name="expectedMessage">The expected exception message</param>
    [Theory]
    [InlineData("This service cannot be constructed without a repository")]
    
    public void CreateInvalidCategoryServiceWithoutRepository(string expectedMessage)
    {
        //act & arrange
        var action = () => new CategoryService(null, mapper, putCategoryValidator, postCategoryValidator);
        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    /// <summary>
    /// Tests whether we get an exception when we try to create a Category service without a mapper
    /// </summary>
    /// <param name="expectedMessage">The expected exception message</param>
    [Theory]
    [InlineData("This service cannot be constructed without a mapper")]
    public void CreateInvalidCategoryServiceWithoutMapper(string expectedMessage)
    {
        //Arrange
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        //Act
        var action = () => new CategoryService(mockRepo.Object, null, putCategoryValidator, postCategoryValidator);
        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    /// <summary>
    /// Tests whether we get an exception when we try to create a Category service without a putValidator
    /// </summary>
    /// <param name="expectedMessage">The expected exception message</param>
    [Theory]
    [InlineData("This service cannot be constructed without a putValidator")]
    public void CreateInvalidCategoryServiceWithoutPutValidator(string expectedMessage)
    {
        //Arrange
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        //Act
        var action = () => new CategoryService(mockRepo.Object, mapper, null, postCategoryValidator);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }

    /// <summary>
    /// Tests whether we get an exception when we try to create a Category service without a postValidator
    /// </summary>
    /// <param name="expectedMessage">The expected exception message</param>
    [Theory]
    [InlineData("This service cannot be constructed without a postValidator")]
    public void CreateInvalidCategoryServiceWithoutPostValidator(string expectedMessage)
    {
        //Arrange
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        //Act
        var action = () => new CategoryService(mockRepo.Object, mapper, putCategoryValidator, null);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    /// <summary>
    /// Tests whether our getAllCategories test can get a list of categories
    /// </summary>
    /// <param name="data"></param>
    /// <param name="expectedResult"></param>
    
    [Theory]
    [MemberData((nameof(GetAllCategories_Test)))]
    public void GetAllCategoriesValidTest(Category[] data, List<Category> expectedResult)
    {
        //Arrange
        var fakerepo = data;
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        mockRepo.Setup(u => u.GetAllCategories()).Returns(fakerepo.ToList);
        //Act
        var actualResult = service.GetAllCategories();
        //Assert
        Assert.Equal(expectedResult, actualResult);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actualResult));
        mockRepo.Verify(r=>r.GetAllCategories(), Times.Once);
    }

    /// <summary>
    /// Tests whether we can create a valid category
    /// </summary>
    /// <param name="categoryId">the categori id being injected into the category´</param>
    [Theory]
    [InlineData(1)]
    public void CreateValidCategoryTest(int categoryId)
    {
        //Arrange
        Category category = new Category{Id = categoryId, CategoryName = "Sko"};
        PostCategoryDTO dto = new PostCategoryDTO()
        {
            CategoryName = category.CategoryName
        };
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        mockRepo.Setup(r => r.CreateCategory(It.IsAny<Category>())).Returns(category);
        //Act
        var categoryCreated = service.CreateCategory(dto);
        //Assert
        Assert.Equal(category.Id, categoryCreated.Id);
        Assert.Equal(category.CategoryName, categoryCreated.CategoryName);
        mockRepo.Verify(r=>r.CreateCategory(It.IsAny<Category>()), Times.Once);
    }

    /// <summary>
    /// Tests whether we get the correct validationexception, when we try to create
    /// a category without a valid categoryName
    /// </summary>
    /// <param name="categoryName">the invalid category name</param>
    /// <param name="expectedExceptionMsg">the expected validationException</param>
    [Theory]
    [InlineData("", typeof(ValidationException))]
    [InlineData(null, typeof(ValidationException))]
    public void CreateInvalidCategoryTest(string categoryName, Type expectedExceptionMsg)
    {
        //Arrange
        PostCategoryDTO dto = new PostCategoryDTO()
        {
            CategoryName = categoryName
        };
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        //Act
        var ex = Assert.Throws<ValidationException>(() => service.CreateCategory(dto));
        //Assert
        Assert.Equal(expectedExceptionMsg, ex.GetType());
    }

    /// <summary>
    /// Tests whether we can update a category
    /// </summary>
    [Fact]
    public void UpdateCategoryValidTest()
    {
        //Arrange
        Category category = new Category { Id = 1, CategoryName = "Biler" };
        //updated categoryname
        PutCategoryDTO dto = new PutCategoryDTO { Id = category.Id, CategoryName = "UpdatedCategoryName" };
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        mockRepo.Setup(r => r.UpdateCategory(category.Id, It.IsAny<Category>())).Returns(category);
        //Act
        
        Category updateCategory = service.UpdateCategory(category.Id, dto);
        //Assert
        Assert.Equal(category, updateCategory);
        Assert.Equal(category.Id, updateCategory.Id);
        Assert.Equal(category.CategoryName, updateCategory.CategoryName);
        mockRepo.Verify(r=>r.UpdateCategory(category.Id, It.IsAny<Category>()),Times.Once);
    }
    
    /// <summary>
    /// Testing whether we get the correct exception message,
    /// when we try to update a category without a valid id, and categoryName
    /// </summary>
    /// <param name="categoryId">the invalid categoryId</param>
    /// <param name="categoryName">the invalid categoryName </param>
    /// <param name="expectedMessage">the expected validationException</param>
    [Theory]
    [InlineData(0, "Biler", typeof(ValidationException))]
    [InlineData(-1, "Sko", typeof(ValidationException))]
    [InlineData(null, "Sko", typeof(ValidationException))]
    [InlineData(1, null, typeof(ValidationException))]
    [InlineData(2, "", typeof(ValidationException))]
    public void UpdateCategoryInvalidTest(int categoryId, string categoryName, Type expectedMessage)
    {
        //Arrange
        PutCategoryDTO dto = new PutCategoryDTO { Id = categoryId, CategoryName = categoryName };
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        //Act
        var action = () => service.UpdateCategory(categoryId, dto);
        //Assert
        var ex = Assert.Throws<ValidationException>(action);
        Assert.Equal(expectedMessage, ex.GetType());
    }
    
    /// <summary>
    /// Tests whether we can delete a category by deleting a deleting a category
    /// from a list of categories
    /// </summary>
    /// <param name="exeptedListSize">the expected listSize</param>
    [Theory]
    [InlineData(1)]
    public void DeleteValidCategoryTest(int exeptedListSize)
    {
        //Arrange
        List<Category> categories = new List<Category>();
        Category category1 = new Category{Id = 1, CategoryName = "Katte"};
        Category categoryToDelete = new Category{Id = 2, CategoryName = "Blomster"};
        categories.Add(category1);
        categories.Add(categoryToDelete);
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);

        mockRepo.Setup(r => r.GetAllCategories()).Returns(categories);
        mockRepo.Setup(r => r.DeleteCategory(categoryToDelete.Id)).Returns(() =>
        {
            categories.Remove(categoryToDelete);
            return categoryToDelete;
        });
        //Act
        var actual = service.DeleteCategory(2);
        //Assert
        Assert.Equal(exeptedListSize, categories.Count);
        Assert.Equal(categoryToDelete, actual);
        Assert.DoesNotContain(categoryToDelete, categories);
        mockRepo.Verify(r=>r.DeleteCategory(2), Times.Once);
    }

    /// <summary>
    /// Tests whether we get the correct exceptionMessage
    /// when we try to delete a category without a valid Id
    /// </summary>
    /// <param name="categoryId">the invalid CategoryId </param>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData(-1, "The category is not found")]
    [InlineData(0, "The category is not found")]
    [InlineData(null, "The category is not found")]
    public void DeleteInvalidCategoryTest(int categoryId, string expectedMessage)
    {
        //Arrange
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        //Act
        var action = () => service.DeleteCategory(categoryId);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
}