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
        SubCategory subcategory1 = new SubCategory() { Id = 1, SubName = "TestSubCategory 1", CategoryID = 1 };
        SubCategory subcategory2 = new SubCategory() { Id = 2, SubName = "TestSubCategory 2", CategoryID = 1 };
        SubCategory subcategory3 = new SubCategory() { Id = 3, SubName = "TestSubCategory 3", CategoryID = 1 };
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
            SubName = subCategory.SubName,
            CategoryID = subCategory.CategoryID
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
        mockRepo.Verify(r => r.addSubCategoryToCategory(It.IsAny<SubCategory>()), Times.Once);
    }

    [Theory]
    [InlineData("", 1, "This SubCategory name is empty/null")]
    [InlineData(null, 1, "This SubCategory name is empty/null")]
    public void CreateInvalidSubCategoryMissingName(string subCategoryName, int categoryID, string expectedException)
    {
        PostSubCategoryDTO dto = new PostSubCategoryDTO()
        {
            SubName = subCategoryName,
            CategoryID = categoryID
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

        var ex = Assert.Throws<ArgumentException>(() => service.addSubCategoryToCategory(dto));

        Assert.Equal("This SubCategory name is empty/null", ex.Message);
    }

    [Theory]
    [InlineData("Test1", "This subcategory needs to be linked with a Category")]
    [InlineData("Test2", "This subcategory needs to be linked with a Category")]
    [InlineData("Test3", "This subcategory needs to be linked with a Category")]
    public void CreateInvalidSubCategoryMissingeCategoryID(string subCategoryName, string expectedException)
    {
        PostSubCategoryDTO dto = new PostSubCategoryDTO()
        {
            SubName = subCategoryName,
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

        var ex = Assert.Throws<ArgumentException>(() => service.addSubCategoryToCategory(dto));

        Assert.Equal("This subcategory needs to be linked with a Category", ex.Message);
    }

    [Theory]
    [InlineData(1)]
    public void DeleteSubCategoryValidTest(int expectedListSize)
    {
        List<SubCategory> subCategories = new List<SubCategory>();

        SubCategory subCategory = new SubCategory { Id = 1, SubName = "Katte", CategoryID = 1 };
        SubCategory subCategoryToDelete = new SubCategory { Id = 2, SubName = "Blomster", CategoryID = 1 };
        subCategories.Add(subCategory);
        subCategories.Add(subCategoryToDelete);
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

        mockRepo.Setup(r => r.GetAllSubCategoriesFromCategory(1)).Returns(subCategories);
        mockRepo.Setup(r => r.deleteSubCategoryFromCategory(subCategoryToDelete.Id)).Returns(() =>
        {
            subCategories.Remove(subCategoryToDelete);
            return subCategoryToDelete;
        });

        var actual = service.deleteSubCategoryFromCategory(2);

        Assert.Equal(expectedListSize, subCategories.Count);
        Assert.Equal(subCategoryToDelete, actual);
        Assert.DoesNotContain(subCategoryToDelete, subCategories);
        mockRepo.Verify(r => r.deleteSubCategoryFromCategory(2), Times.Once);
    }

    [Theory]
    [InlineData(0, "The SubCategory id is not found")]
    [InlineData(null, "The SubCategory id is not found")]
    public void DeleteSubCategoryInvalidTest(int subCategoryID, string expectedException)
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

        var action = () => service.deleteSubCategoryFromCategory(subCategoryID);

        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedException, ex.Message);
    }

    [Theory]
    [InlineData(1, "Sko", 1)]
    public void UpdateSubCategoryValidTest(int id, string categoryName, int categoryId)
    {
        SubCategory subCategory = new SubCategory() { Id = 1, SubName = "Biler", CategoryID = 2 };
        PutSubCategoryDTO dto = new PutSubCategoryDTO()
            { Id = subCategory.Id, SubName = subCategory.SubName, CategoryID = subCategory.CategoryID };
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
        mockRepo.Setup(r => r.updateSubCategory(id, It.IsAny<SubCategory>())).Returns(subCategory);

        dto.SubName = categoryName;
        SubCategory updateSubCategory = service.updateSubCategory(id, dto);

        Assert.Equal(subCategory, updateSubCategory);
        Assert.Equal(subCategory.Id, updateSubCategory.Id);
        Assert.Equal(subCategory.SubName, updateSubCategory.SubName);
        mockRepo.Verify(r => r.updateSubCategory(id, It.IsAny<SubCategory>()), Times.Once);
    }

    [Theory]
    [InlineData(0, "Biler", 1, "This subcategroy is not found")]
    [InlineData(-1, "Sko", 1, "This subcategroy is not found")]
    [InlineData(null, "Sko", 1, "This subcategroy is not found")]
    [InlineData(1, null, 1, "This SubCategory name is empty/null")]
    [InlineData(1, "", 1, "This SubCategory name is empty/null")]
    [InlineData(1, "Katte", 0, "This subcategory needs to be linked with a Category")]
    [InlineData(1, "Hunde", null, "This subcategory needs to be linked with a Category")]
    [InlineData(1, "Biler", -1, "This subcategory needs to be linked with a Category")]
    public void UpdateSubCategoryInvalidTest(int id, string categoryName, int categoryId, string expectedMessage)
    {
        PutSubCategoryDTO dto = new PutSubCategoryDTO()
            { Id = id, SubName = categoryName, CategoryID = categoryId };
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

        var action = () => service.updateSubCategory(id, dto);

        var ex = Assert.Throws<ArgumentException>(action);
        
        Assert.Equal(expectedMessage, ex.Message);
    }
    
}