using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace RedPaperUnitTest;

public class SubCategoryTest
{
    public static IEnumerable<Object[]> GetAllSubCategories_Test()
    {
        SubCategory subcategory1 = new SubCategory() { Id = 1, SubName = "TestSubCategory 1", CategoryID = 1};
        SubCategory subcategory2 = new SubCategory() { Id = 2, SubName = "TestSubCategory 2", CategoryID = 1};
        SubCategory subcategory3 = new SubCategory() { Id = 3, SubName = "TestSubCategory 3", CategoryID = 1};
        yield return new object[]
        {
            new SubCategory[]
            {
                subcategory1,
                subcategory2,
                subcategory3
            },
            new List<SubCategory>() { subcategory1, subcategory2, subcategory3 }
        };
    } 
    
    [Fact]
    public void CreateSubCategoryServiceTest()
    {
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostSubCategoryDTO, SubCategory>();
            config.CreateMap<PutSubCategoryDTO, SubCategory>();
        }).CreateMapper();
        var postSubCategoryValidator = new SubCategoryValidator.PostSubCategoryValidator();
        var putSubCategoryValidator = new SubCategoryValidator.PutSubCategoryValidator();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        //
        Assert.NotNull(service);
        Assert.True(service is SubCategoryService);
    }

    [Theory]
    [MemberData(nameof(GetAllSubCategories_Test))]
    public void GetAllSubCategoriesTest(SubCategory[] data, List<SubCategory> expectedResult)
    {
        int categoryID = 1;
        var fakeRepo = data;
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostSubCategoryDTO, SubCategory>();
            config.CreateMap<PutSubCategoryDTO, SubCategory>();
        }).CreateMapper();
        var postSubCategoryValidator = new SubCategoryValidator.PostSubCategoryValidator();
        var putSubCategoryValidator = new SubCategoryValidator.PutSubCategoryValidator();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        mockRepo.Setup(s => s.GetAllSubCategoriesFromCategory(categoryID)).Returns(fakeRepo.ToList);
        var actualResult = service.GetAllSubCategoriesFromCategory(1);
        Assert.Equal(expectedResult, actualResult);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actualResult));
        mockRepo.Verify(s => s.GetAllSubCategoriesFromCategory(1), Times.Once);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 2)]
    [InlineData(3, 1)]
    [InlineData(4, 1)]
    public void CreateValidSubCategory(int subCategoryID, int categoryID)
    {
        SubCategory subCategory = new SubCategory { Id = subCategoryID, SubName = "TestSub", CategoryID = categoryID };
        PostSubCategoryDTO dto = new PostSubCategoryDTO()
        {
            SubName = subCategory.SubName
        };
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostSubCategoryDTO, SubCategory>();
            config.CreateMap<PutSubCategoryDTO, SubCategory>();
        }).CreateMapper();
        var postSubCategoryValidator = new SubCategoryValidator.PostSubCategoryValidator();
        var putSubCategoryValidator = new SubCategoryValidator.PutSubCategoryValidator();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);

        mockRepo.Setup(r => r.addSubCategoryToCategory(It.IsAny<SubCategory>())).Returns(subCategory);

        var subCategoryCreated = service.addSubCategoryToCategory(dto);
        
        //Assert
        Assert.Equal(subCategory.Id, subCategoryCreated.Id);
        Assert.Equal(subCategory.CategoryID, subCategoryCreated.CategoryID);
        Assert.Equal(subCategory, subCategoryCreated);
        mockRepo.Verify(r => r.addSubCategoryToCategory( subCategory), Times.Once);
    }
    

    
}