using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace RedPaperUnitTest;

public class CategoryTest
{
    public static IEnumerable<Object[]> GetAllCategories_Test()
    {
        Category category1 = new Category { Id = 1, Name = "TestCategory 1" };
        Category category2 = new Category { Id = 1, Name = "TestCategory 2" };
        Category category3 = new Category { Id = 1, Name = "TestCategory 3" };
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
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutCategoryDTO, Category>();
            config.CreateMap<PostCategoryDTO, Category>();
        }).CreateMapper();
        var putCategoryValidator = new CategoryValidator.CategoryPutValidator();
        var postCategoryValidator = new CategoryValidator.CategoryPostValidator();
        ICategoryService service =
            new CategoryService(mockRepo.Object, mapper, putCategoryValidator, postCategoryValidator);
        //Assert
        Assert.NotNull(service);
        Assert.True(service is CategoryService);
    }

}