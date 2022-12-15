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

public class SubCategoryTest
{
    private IMapper mapper;
    private SubCategoryValidator.PostSubCategoryValidator postSubCategoryValidator;
    private SubCategoryValidator.PutSubCategoryValidator putSubCategoryValidator;

    public SubCategoryTest()
    {
        var _mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostSubCategoryDTO, SubCategory>();
            config.CreateMap<PutSubCategoryDTO, SubCategory>();
        }).CreateMapper();
        mapper = _mapper;

        postSubCategoryValidator = new SubCategoryValidator.PostSubCategoryValidator();
        putSubCategoryValidator = new SubCategoryValidator.PutSubCategoryValidator();
    }

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
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        //
        Assert.NotNull(service);
        Assert.True(service is SubCategoryService);
    }
    
    [Theory]
    [InlineData("This service cannot be constructed without a repository")]
    public void CreateInvalidProductServiceWithoutRepository(string expectedMessage)
    {
        var action = () => new SubCategoryService(null, mapper, postSubCategoryValidator, putSubCategoryValidator);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    [Theory]
    [InlineData("This service cannot be constructed without a mapper")]
    public void CreateInvalidProductServiceWithoutMapper(string expectedMessage)
    {
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        var action = () => new SubCategoryService(mockRepo.Object, null, postSubCategoryValidator, putSubCategoryValidator);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    [Theory]
    [InlineData("This service cannot be constructed without a postValidator")]
    public void CreateInvalidProductServiceWithoutPostValidator(string expectedMessage)
    {
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        var action = () => new SubCategoryService(mockRepo.Object, mapper, null, putSubCategoryValidator);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    [Theory]
    [InlineData("This service cannot be constructed without a putValidator")]
    public void CreateInvalidProductServiceWithoutPutValidator(string expectedMessage)
    {
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        var action = () => new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, null);
        var ex = Assert.Throws<ArgumentException>(action);
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
    

    [Theory]
    [MemberData(nameof(GetAllSubCategories_Test))]
    public void GetAllSubCategoriesTest(SubCategory[] data, List<SubCategory> expectedResult)
    {
        int categoryID = 1;
        var fakeRepo = data;
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
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
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);

        mockRepo.Setup(r => r.AddSubCategoryToCategory(It.IsAny<SubCategory>())).Returns(subCategory);

        var subCategoryCreated = service.AddSubCategoryToCategory(dto);

        //Assert
        Assert.Equal(subCategory.Id, subCategoryCreated.Id);
        Assert.Equal(subCategory.CategoryID, subCategoryCreated.CategoryID);
        Assert.Equal(subCategory, subCategoryCreated);
        mockRepo.Verify(r => r.AddSubCategoryToCategory(It.IsAny<SubCategory>()), Times.Once);
    }

    [Theory]
    [InlineData("", 1, typeof(ValidationException))]
    [InlineData(null, 1, typeof(ValidationException))]
    [InlineData("SubName", null, typeof(ValidationException))]
    [InlineData("SubName", -1, typeof(ValidationException))]
    public void CreateInvalidSubCategoryMissingName(string subCategoryName, int categoryID, Type expectedException)
    {
        PostSubCategoryDTO dto = new PostSubCategoryDTO()
        {
            SubName = subCategoryName,
            CategoryID = categoryID
        };

        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);

        var ex = Assert.Throws<ValidationException>(() => service.AddSubCategoryToCategory(dto));

        Assert.Equal(expectedException, ex.GetType());
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
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        mockRepo.Setup(r => r.GetAllSubCategoriesFromCategory(1)).Returns(subCategories);
        mockRepo.Setup(r => r.DeleteSubCategoryFromCategory(subCategoryToDelete.Id)).Returns(() =>
        {
            subCategories.Remove(subCategoryToDelete);
            return subCategoryToDelete;
        });
        var actual = service.DeleteSubCategoryFromCategory(2);
        Assert.Equal(expectedListSize, subCategories.Count);
        Assert.Equal(subCategoryToDelete, actual);
        Assert.DoesNotContain(subCategoryToDelete, subCategories);
        mockRepo.Verify(r => r.DeleteSubCategoryFromCategory(2), Times.Once);
    }

    [Theory]
    [InlineData(0, "The SubCategory id is not found")]
    [InlineData(null, "The SubCategory id is not found")]
    public void DeleteSubCategoryInvalidTest(int subCategoryID, string expectedException)
    {
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);

        var action = () => service.DeleteSubCategoryFromCategory(subCategoryID);

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
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        mockRepo.Setup(r => r.UpdateSubCategory(id, It.IsAny<SubCategory>())).Returns(subCategory);

        dto.SubName = categoryName; 
        SubCategory updateSubCategory = service.UpdateSubCategory(id, dto);

        Assert.Equal(subCategory, updateSubCategory);
        Assert.Equal(subCategory.Id, updateSubCategory.Id);
        Assert.Equal(subCategory.SubName, updateSubCategory.SubName);
        mockRepo.Verify(r => r.UpdateSubCategory(id, It.IsAny<SubCategory>()), Times.Once);
    }

    [Theory]
    [InlineData(0, "Biler", 1, typeof(ValidationException))]
    [InlineData(-1, "Sko", 1, typeof(ValidationException))]
    [InlineData(null, "Sko", 1, typeof(ValidationException))]
    [InlineData(1, null, 1, typeof(ValidationException))]
    [InlineData(1, "", 1, typeof(ValidationException))]
    [InlineData(1, "Katte", 0, typeof(ValidationException))]
    [InlineData(1, "Hunde", null, typeof(ValidationException))]
    [InlineData(1, "Biler", -1,typeof(ValidationException))]
    public void UpdateSubCategoryInvalidTest(int id, string subCategoryName, int categoryId, Type expectedMessage)
    {
        PutSubCategoryDTO dto = new PutSubCategoryDTO()
            { Id = id, SubName = subCategoryName, CategoryID = categoryId };
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        var action = () => service.UpdateSubCategory(id, dto);
        var ex = Assert.Throws<ValidationException>(action);
        Assert.Equal(expectedMessage, ex.GetType());
    }
    
}