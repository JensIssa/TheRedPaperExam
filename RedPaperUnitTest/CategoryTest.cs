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

    [Fact]
    public void CreateCategoryService()
    {
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        //Assert
        Assert.NotNull(service);
        Assert.True(service is CategoryService);
    }

    [Theory]
    [InlineData("This service cannot be constructed without a repository")]
    
    public void CreateInvalidCategoryServiceWithoutRepository(string expectedMessage)
    {
        var action = () => new CategoryService(null, mapper, putCategoryValidator, postCategoryValidator);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    [Theory]
    [InlineData("This service cannot be constructed without a mapper")]
    public void CreateInvalidCategoryServiceWithoutMapper(string expectedMessage)
    {
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        var action = () => new CategoryService(mockRepo.Object, null, putCategoryValidator, postCategoryValidator);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
    [Theory]
    [InlineData("This service cannot be constructed without a putValidator")]
    public void CreateInvalidCategoryServiceWithoutPutValidator(string expectedMessage)
    {
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        var action = () => new CategoryService(mockRepo.Object, mapper, null, postCategoryValidator);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [InlineData("This service cannot be constructed without a postValidator")]
    public void CreateInvalidCategoryServiceWithoutPostValidator(string expectedMessage)
    {
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        var action = () => new CategoryService(mockRepo.Object, mapper, putCategoryValidator, null);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    [Theory]
    [MemberData((nameof(GetAllCategories_Test)))]
    public void GetAllCategoriesTest(Category[] data, List<Category> expectedResult)
    {
        var fakerepo = data;
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        mockRepo.Setup(u => u.GetAllCategories()).Returns(fakerepo.ToList);
        var actualResult = service.GetAllCategories();
        Assert.Equal(expectedResult, actualResult);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actualResult));
        mockRepo.Verify(r=>r.GetAllCategories(), Times.Once);
    }

    [Theory]
    [InlineData(1)]
    public void CreateValidCategoryTest(int categoryId)
    {
        Category category = new Category{Id = categoryId, CategoryName = "Sko"};
        PostCategoryDTO dto = new PostCategoryDTO()
        {
            CategoryName = category.CategoryName
        };
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        mockRepo.Setup(r => r.CreateCategory(It.IsAny<Category>())).Returns(category);
        var categoryCreated = service.CreateCategory(dto);
        
        //Assert
        Assert.Equal(category.Id, categoryCreated.Id);
        Assert.Equal(category.CategoryName, categoryCreated.CategoryName);
        mockRepo.Verify(r=>r.CreateCategory(It.IsAny<Category>()), Times.Once);
    }

    [Theory]
    [InlineData("", typeof(ValidationException))]
    [InlineData(null, typeof(ValidationException))]
    public void CreateInvalidCategoryTest(string categoryName, Type expectedExceptionMsg)
    {
        PostCategoryDTO dto = new PostCategoryDTO()
        {
            CategoryName = categoryName
        };
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        var ex = Assert.Throws<ValidationException>(() => service.CreateCategory(dto));
        
        Assert.Equal(expectedExceptionMsg, ex.GetType());
    }

    [Theory]
    [InlineData(1, "Sko")]
    public void UpdateCategoryValidTest(int id, string categoryName)
    {
        Category category = new Category { Id = 1, CategoryName = "Biler" };
        PutCategoryDTO dto = new PutCategoryDTO { Id = category.Id, CategoryName = category.CategoryName };
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        mockRepo.Setup(r => r.UpdateCategory(id, It.IsAny<Category>())).Returns(category);

        dto.CategoryName = categoryName;
        Category updateCategory = service.UpdateCategory(id, dto);
        
        Assert.Equal(category, updateCategory);
        Assert.Equal(category.Id, updateCategory.Id);
        Assert.Equal(category.CategoryName, updateCategory.CategoryName);
        mockRepo.Verify(r=>r.UpdateCategory(id, It.IsAny<Category>()),Times.Once);
    }
    

    [Theory]
    [InlineData(0, "Biler", typeof(ValidationException))]
    [InlineData(-1, "Sko", typeof(ValidationException))]
    [InlineData(null, "Sko", typeof(ValidationException))]
    [InlineData(1, null, typeof(ValidationException))]
    [InlineData(2, "", typeof(ValidationException))]
    public void UpdateCategoryInvalidTest(int categoryId, string categoryName, Type expectedMessage)
    {
        PutCategoryDTO dto = new PutCategoryDTO { Id = categoryId, CategoryName = categoryName };
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        var action = () => service.UpdateCategory(categoryId, dto);
        var ex = Assert.Throws<ValidationException>(action);
        
        Assert.Equal(expectedMessage, ex.GetType());
    }
    
    
    [Theory]
    [InlineData(1)]
    public void DeleteValidCategoryTest(int exeptedListSize)
    {
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
        var actual = service.DeleteCategory(2);
        Assert.Equal(exeptedListSize, categories.Count);
        Assert.Equal(categoryToDelete, actual);
        Assert.DoesNotContain(categoryToDelete, categories);
        mockRepo.Verify(r=>r.DeleteCategory(2), Times.Once);
    }

    [Theory]
    [InlineData(-1, "The category is not found")]
    [InlineData(0, "The category is not found")]
    [InlineData(null, "The category is not found")]
    public void DeleteInvalidCategoryTest(int categoryId, string expectedMessage)
    {
        Mock<ICategoryRepository> mockRepo = new Mock<ICategoryRepository>();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);

        var action = () => service.DeleteCategory(categoryId);

        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
}