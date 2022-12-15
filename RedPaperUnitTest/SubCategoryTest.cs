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

    /// <summary>
    /// Initilizating the mapper, postValidator and putValidator
    /// </summary>
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

    /// <summary>
    /// A list used for the getAllSubcategories test
    /// </summary>
    /// <returns>an IEnumerable Object list</returns>
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

    /// <summary>
    /// Tests whether we can create a SubcategoryService
    /// </summary>
    [Fact]
    public void CreateSubCategoryServiceTest()
    {
        //Arrange & act
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        //Assert
        Assert.NotNull(service);
        Assert.True(service is SubCategoryService);
    }
    /// <summary>
    /// Tests whether we get the correct exceptionMessage out, when we try to create
    /// a subcategoryservice without a repository
    /// </summary>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData("This service cannot be constructed without a repository")]
    public void CreateInvalidProductServiceWithoutRepository(string expectedMessage)
    { 
        //Arrange and Act
        var action = () => new SubCategoryService(null, mapper, postSubCategoryValidator, putSubCategoryValidator);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    /// <summary>
    /// Tests whether we get the correct exceptionMessage out, when we try to create
    /// a subcategoryservice without a mapper
    /// </summary>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData("This service cannot be constructed without a mapper")]
    public void CreateInvalidProductServiceWithoutMapper(string expectedMessage)
    {
        //Arrange
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        //Act
        var action = () => new SubCategoryService(mockRepo.Object, null, postSubCategoryValidator, putSubCategoryValidator);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    /// <summary>
    /// Tests whether we get the correct exceptionMessage out, when we try to create
    /// a subcategoryservice without a postValidator
    /// </summary>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData("This service cannot be constructed without a postValidator")]
    public void CreateInvalidProductServiceWithoutPostValidator(string expectedMessage)
    {
        //Arrange
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        //Act
        var action = () => new SubCategoryService(mockRepo.Object, mapper, null, putSubCategoryValidator);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    /// <summary>
    /// Tests whether we get the correct exceptionMessage out, when we try to create
    /// a subcategoryservice without a putValidator
    /// </summary>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData("This service cannot be constructed without a putValidator")]
    public void CreateInvalidProductServiceWithoutPutValidator(string expectedMessage)
    {
        //Arrange
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        //Act
        var action = () => new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, null);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    /// <summary>
    /// Tests whether we can get all subcategories
    /// </summary>
    /// <param name="data">the subcategoryList we are getting</param>
    /// <param name="expectedResult">the expected subCategory list</param>
    [Theory]
    [MemberData(nameof(GetAllSubCategories_Test))]
    public void GetAllSubCategoriesTest(SubCategory[] data, List<SubCategory> expectedResult)
    {
        //Arrange
        int categoryID = 1;
        var fakeRepo = data;
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        mockRepo.Setup(s => s.GetAllSubCategoriesFromCategory(categoryID)).Returns(fakeRepo.ToList);
        //Act
        var actualResult = service.GetAllSubCategoriesFromCategory(1);
        //Assert
        Assert.Equal(expectedResult, actualResult);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actualResult));
        mockRepo.Verify(s => s.GetAllSubCategoriesFromCategory(1), Times.Once);
    }

    /// <summary>
    /// Tests whether we can create a valid subCategory
    /// </summary>
    /// <param name="subCategoryID"> the subcategoryId</param>
    /// <param name="categoryID">the categoryId</param>
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 2)]
    [InlineData(3, 1)]
    [InlineData(4, 1)]
    public void CreateValidSubCategory(int subCategoryID, int categoryID)
    {
        //Arrange
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
        //Act
        var subCategoryCreated = service.AddSubCategoryToCategory(dto);

        //Assert
        Assert.Equal(subCategory.Id, subCategoryCreated.Id);
        Assert.Equal(subCategory.CategoryID, subCategoryCreated.CategoryID);
        Assert.Equal(subCategory, subCategoryCreated);
        mockRepo.Verify(r => r.AddSubCategoryToCategory(It.IsAny<SubCategory>()), Times.Once);
    }

    /// <summary>
    /// Tests whether we get the correct ValidationException out
    /// when we insert an invalid input 
    /// </summary>
    /// <param name="subCategoryName">the invalid subCategoryName</param>
    /// <param name="categoryID">the invalid categoryId</param>
    /// <param name="expectedException">the expected exception</param>
    [Theory]
    [InlineData("", 1, typeof(ValidationException))]
    [InlineData(null, 1, typeof(ValidationException))]
    [InlineData("SubName", null, typeof(ValidationException))]
    [InlineData("SubName", -1, typeof(ValidationException))]
    public void CreateInvalidSubCategoryMissingName(string subCategoryName, int categoryID, Type expectedException)
    {
        //Arrange & act
        PostSubCategoryDTO dto = new PostSubCategoryDTO()
        {
            SubName = subCategoryName,
            CategoryID = categoryID
        };

        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        //Assert
        var ex = Assert.Throws<ValidationException>(() => service.AddSubCategoryToCategory(dto));
        Assert.Equal(expectedException, ex.GetType());
    }

    /// <summary>
    /// Tests whether we can delete a subCategory from a list
    /// </summary>
    /// <param name="expectedListSize">the expected listSize when a subcategory gets deleted</param>
    [Theory]
    [InlineData(1)]
    public void DeleteSubCategoryValidTest(int expectedListSize)
    {
        //Arrange
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
        //Act
        var actual = service.DeleteSubCategoryFromCategory(2);
        //Assert
        Assert.Equal(expectedListSize, subCategories.Count);
        Assert.Equal(subCategoryToDelete, actual);
        Assert.DoesNotContain(subCategoryToDelete, subCategories);
        mockRepo.Verify(r => r.DeleteSubCategoryFromCategory(2), Times.Once);
    }

    /// <summary>
    /// Tests whether when we input an invalid subCategoryId
    /// if we get the correct exceptionMessage out
    /// </summary>
    /// <param name="subCategoryID">the invalid subCategoryId</param>
    /// <param name="expectedException">the expected exception message</param>
    [Theory]
    [InlineData(0, "The SubCategory id is not found")]
    [InlineData(null, "The SubCategory id is not found")]
    public void DeleteSubCategoryInvalidTest(int subCategoryID, string expectedException)
    {
        //Arrange
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        //Act
        var action = () => service.DeleteSubCategoryFromCategory(subCategoryID);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedException, ex.Message);
    }

    /// <summary>
    /// Tests whether we can update a valid subCategory
    /// </summary>
    /// <param name="id">the Id being updated</param>
    /// <param name="categoryName">the CategoryName being updated</param>
    /// <param name="categoryId">the categoryId being updated</param>
    [Theory]
    [InlineData(1, "Sko", 1)]
    public void UpdateSubCategoryValidTest(int id, string categoryName, int categoryId)
    {
        //Arrange
        SubCategory subCategory = new SubCategory() { Id = 1, SubName = "Biler", CategoryID = 2 };
        PutSubCategoryDTO dto = new PutSubCategoryDTO()
            { Id = subCategory.Id, SubName = subCategory.SubName, CategoryID = subCategory.CategoryID };
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        mockRepo.Setup(r => r.UpdateSubCategory(id, It.IsAny<SubCategory>())).Returns(subCategory);

        dto.SubName = categoryName; 
        //Act
        SubCategory updateSubCategory = service.UpdateSubCategory(id, dto);
        //Assert
        Assert.Equal(subCategory, updateSubCategory);
        Assert.Equal(subCategory.Id, updateSubCategory.Id);
        Assert.Equal(subCategory.SubName, updateSubCategory.SubName);
        mockRepo.Verify(r => r.UpdateSubCategory(id, It.IsAny<SubCategory>()), Times.Once);
    }

    /// <summary>
    /// Tests whether we can get the correct validationException out,
    /// when we input an invalid value
    /// </summary>
    /// <param name="id">the valid and invalid inputs</param>
    /// <param name="subCategoryName">the valid and invalid inputs</param>
    /// <param name="categoryId">the valid and invalid inputs</param>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
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
        //Arrange
        PutSubCategoryDTO dto = new PutSubCategoryDTO()
            { Id = id, SubName = subCategoryName, CategoryID = categoryId };
        Mock<ISubCategoryRepository> mockRepo = new Mock<ISubCategoryRepository>();
        ISubCategoryService service =
            new SubCategoryService(mockRepo.Object, mapper, postSubCategoryValidator, putSubCategoryValidator);
        //Act
        var action = () => service.UpdateSubCategory(id, dto);
        //Assert
        var ex = Assert.Throws<ValidationException>(action);
        Assert.Equal(expectedMessage, ex.GetType());
    }
    
}