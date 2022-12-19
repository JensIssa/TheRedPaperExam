using Application.DTOs;
using Application.Helpers;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Options;
using Moq;

namespace RedPaperUnitTest;

public class UserTest
{

    private UserValidator.UserPostValidator postUserValiator;
    private UserValidator.UserPutValidator putUserValidator;
    private IMapper _mapper;

    /// <summary>
    /// Initiliaze the mapper, postValidator and putValidator in the constructor
    /// </summary>
    public UserTest()
    {
        postUserValiator = new UserValidator.UserPostValidator();
        putUserValidator = new UserValidator.UserPutValidator();

        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutUserDTO, User>();
            config.CreateMap<RegisterDTO, User>();
        }).CreateMapper();
        _mapper = mapper;
    }

    /// <summary>
    /// The getAllUsers list being used, when we test the GetAllUser method
    /// </summary>
    /// <returns>an IEnumrable list</returns>
    public static IEnumerable<Object[]> GetALlUsers_Test()
    {
        User user1 = new User{Id = 1, AssignedRole = "Customer", BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Esbjerg", Username = "JensIKørestol", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user2 = new User{Id = 2, AssignedRole = "Admin", BirthDay = new DateTime(2005, 10, 15 ), Email = "andreasberthelsen@hotmail.com", FirstName = "Andreas", Hash = " ", LastName = "Berthelsen",Username = "BerthelDenSeje", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user3 = new User{Id = 3, AssignedRole = "Admin", BirthDay = new DateTime(2007, 10, 15 ), Email = "mathiasmadsen@hotmail.com", FirstName = "Mathias", Hash = " ", LastName = "Madsen",Username = "MathiasDenEmo", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        yield return new object[]
        {
            new User[]
            {
                user1,
                user2, 
                user3
            },
            new List<User>() { user1, user2, user3 }
        };
    }

    /// <summary>
    /// Tests whether we can create a UserService
    /// </summary>
    [Fact]
    public void CreateUserServiceTest()
    {
        //Arrange and act
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        var putUserValidator = new UserValidator.UserPutValidator();
        var postUserValiator = new UserValidator.UserPostValidator();
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator, _mapper);
        //Assert
        Assert.NotNull(service);
        Assert.True(service is UserService);
    }
    
    /// <summary>
    /// Tests whether we get the correct exceptionMessage out, when we try to create
    /// a userService without a repository
    /// </summary>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData("This service cannot be constructed without a repository")]
    public void CreateInvalidUserServiceWithoutRepository(string expectedMessage)
    { 
        //Arrange and Act
        var action = () => new UserService(null, putUserValidator, postUserValiator, _mapper);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }

    /// <summary>
    /// Tests whether we get the correct exceptionMessage out, when we try to create
    /// a userService without a putValidator
    /// </summary>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData("This service cannot be constructed without a putValidator")]
    public void CreateInvalidUserServiceWithoutPutValidator(string expectedMessage)
    {
        //Arrange and Act
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        var action = () => new UserService(mockRepo.Object, null, postUserValiator, _mapper);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    /// <summary>
    /// Tests whether we get the correct exceptionMessage out, when we try to create
    /// a userService without a repository
    /// </summary>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData("This service cannot be constructed without a postValidator")]
    public void CreateInvalidUserServiceWithoutPostValidator(string expectedMessage)
    {
        //Arrange and Act
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        var action = () => new UserService(mockRepo.Object, putUserValidator, null, _mapper);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    /// <summary>
    /// Tests whether we get the correct exceptionMessage out, when we try to create
    /// a userService without a repository
    /// </summary>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData("This service cannot be constructed without a mapper")]
    public void CreateInvalidUserServiceWithoutMapper(string expectedMessage)
    {
        //Arrange and Act
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        var action = () => new UserService(mockRepo.Object, putUserValidator, postUserValiator, null);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    /// <summary>
    /// Tests whether we can get all the users from the getAllUsers method
    /// </summary>
    /// <param name="data"> the data to the getAllUsers method</param>
    /// <param name="expectedResult">the expected list of users</param>
    [Theory]
    [MemberData(nameof(GetALlUsers_Test))]
    public void GetAllUsers(User[] data, List<User> expectedResult)
    {
        //Arrange
        var fakerepo = data;

        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator, _mapper);
        mockRepo.Setup(u => u.GetAllUsers()).Returns(fakerepo.ToList);
        //Act
        var actualResult = service.GetAllUsers();
        //Assert
        Assert.Equal(expectedResult, actualResult);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actualResult));
        mockRepo.Verify(r => r.GetAllUsers(), Times.Once);
    }
    
    /// <summary>
    /// Tests whether we can get a user by their username
    /// </summary>
    /// <param name="userUsername">the userName being looked after</param>
    /// <param name="expectedResult">the expected result</param>
    [Theory]
    [InlineData("JensIKørestol","JensIKørestol")]
    [InlineData("BerthelDenSeje","BerthelDenSeje")]
    [InlineData("MathiasDenEmo","MathiasDenEmo")]
    public void ValidGetUserByUsernameTest(string userUsername, string expectedResult)
    {
        //Arrange
        User user1 = new User{Id = 1, AssignedRole = "Customer", BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Esbjerg", Username = "JensIKørestol", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user2 = new User{Id = 2, AssignedRole = "Admin", BirthDay = new DateTime(2005, 10, 15 ), Email = "andreasberthelsen@hotmail.com", FirstName = "Andreas", Hash = " ", LastName = "Berthelsen",Username = "BerthelDenSeje", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user3 = new User{Id = 3, AssignedRole = "Admin", BirthDay = new DateTime(2007, 10, 15 ), Email = "mathiasmadsen@hotmail.com", FirstName = "Mathias", Hash = " ", LastName = "Madsen",Username = "MathiasDenEmo", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};

        var fakeRepo = new List<User>();
        fakeRepo.Add(user1);
        fakeRepo.Add(user2);
        fakeRepo.Add(user3);
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator, _mapper);
        mockRepo.Setup(u => u.GetUserByUsername(userUsername)).Returns(fakeRepo.Find(u => u.Username == userUsername));
        //Act        
        var actual = service.GetUserByUsername(userUsername);
        //Assert
        Assert.Equal(expectedResult, actual.Username);
        mockRepo.Verify(r => r.GetUserByUsername(userUsername), Times.Once);
    }

    /// <summary>
    /// Tests whether we can get the correct exceptionMessage out,
    /// when we insert an invalid input
    /// </summary>
    /// <param name="username">the invalid username</param>
    /// <param name="expectedExceptionMsg">the expected exception message</param>
    [Theory]
    [InlineData("", "Username is empty or null")]
    [InlineData(null, "Username is empty or null")]
    public void InvalidGetUserByUsername(string username, string expectedExceptionMsg)
    {
        //Arrange
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator, _mapper);
        //Act
        var ex = Assert.Throws<ArgumentException>(() => service.GetUserByUsername(username));
        //Assert
        Assert.Equal("Username is empty or null", ex.Message);
        mockRepo.Verify(r => r.GetUserByUsername(username), Times.Never);
    }
    
    /// <summary>
    /// Tests whether we can delete a user from a list
    /// </summary>
    /// <param name="expectedListSize">the expected list size after a user is deleted</param>
    [Theory]
    [InlineData(1)] // Delete user with id 1 and expectedListSize 
    public void DeleteValidUserTest(int expectedListSize)
    {
        //Arrange
        List<User> users = new List<User>();
        User toBeDeleted = new User{Id = 1, AssignedRole = "Customer", BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Issa", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user2 = new User{Id = 2, AssignedRole = "Customer", BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Issa", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};

        users.Add(toBeDeleted);
        users.Add(user2);
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator, _mapper);
        mockRepo.Setup(r => r.GetAllUsers()).Returns(users);
        mockRepo.Setup(r => r.DeleteUser(toBeDeleted.Id)).Returns(() =>
        {
            //Act
            users.Remove(toBeDeleted);
            return toBeDeleted;
        });
        //Act
        var actual = service.DeleteUser(1);
        
        //Assert
        Assert.Equal(expectedListSize, users.Count);
        Assert.Equal(toBeDeleted, actual);
        Assert.DoesNotContain(toBeDeleted, users);
        mockRepo.Verify(r=> r.DeleteUser(1), Times.Once);
    }
    
    /// <summary>
    /// Tests whether we can delete a user,
    /// when the Id is null or under 1
    /// </summary>
    /// <param name="userId">the invalid userId</param>
    /// <param name="expectedMessage">the expected exception mesasge</param>
    [Theory]
    [InlineData(-1, "ID does not exist or is null")]   
    [InlineData(0, "ID does not exist or is null")]    
    [InlineData(null, "ID does not exist or is null")] 
    public void DeleteInvalidUserTest(int userId, string expectedMessage)
    {
        // Arrange
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator, _mapper);

        // Act 
        var action = () => service.DeleteUser(userId);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
        mockRepo.Verify(r=>r.DeleteUser(userId),Times.Never);
    }
    
    /// <summary>
    /// Tests whether we can update a user 
    /// </summary>
    [Fact]
    public void UpdateValidUser()
    {
        User user2 = new User{Id = 2, AssignedRole = "Customer", BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Issa", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " ", Username = "JensIssa"};
        PutUserDTO dto = new PutUserDTO()
        {
            Email = user2.Email,
            FirstName = user2.FirstName,
            LastName = user2.LastName,
            Location = user2.Location,
            PhoneNumber = user2.PhoneNumber,
            Username = user2.Username, 
            BirthDay = user2.BirthDay,
            Id = user2.Id
        };
            Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        IUserService service =
            new UserService(mockRepo.Object, putUserValidator, postUserValiator, _mapper);
        mockRepo.Setup(r => r.UpdateUser(It.IsAny<User>(), dto.Id)).Returns(user2);
        var userCreated = service.UpdateUser(dto.Id, dto);
        //Assert
        Assert.Equal(user2.Id, userCreated.Id);
        Assert.Equal(user2.Username, userCreated.Username);
        mockRepo.Verify(r=>r.CreateNewUser(It.IsAny<User>()), Times.Never);
    }

    /// <summary>
    /// Tests whether we get the correct validationException, when we input
    /// a invalid input
    /// </summary>
    /// <param name="id">the valid and invalid input</param>
    /// <param name="firstName">the valid and invalid input</param>
    /// <param name="lastName">the valid and invalid input</param>
    /// <param name="username">the valid and invalid input</param>
    /// <param name="email">the valid and invalid input</param>
    /// <param name="phoneNumber">the valid and invalid input</param>
    /// <param name="location">the valid and invalid input</param>
    /// <param name="expectedMessage">the expected exceptionMessage</param>
    [Theory]
    [InlineData(0, "Første", "Sidste", "MitUserName", "jensissa1999@gmail.com", 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(null, "Første", "Sidste", "MitUserName", "jensissa1999@gmail.com", 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(1, null, "Sidste", "MitUserName", "jensissa1999@gmail.com", 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(1, "", "Sidste", "MitUserName", "jensissa1999@gmail.com", 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(1, "Første", "", "MitUserName", "jensissa1999@gmail.com", 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(1, "Første", null, "MitUserName", "jensissa1999@gmail.com", 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(1, "Første", "Sidste", null, "jensissa1999@gmail.com", 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(1, "Første", "Sidste", "", "jensissa1999@gmail.com", 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(1, "Første", "Sidste", "MitUserName", null, 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(1, "Første", "Sidste", "MitUserName", "", 75453979, "Esbjerg", typeof(ValidationException))]
    [InlineData(1, "Første", "Sidste", "MitUserName", "jensissa1999@gmail.com", 75453979, "", typeof(ValidationException))]
    [InlineData(1, "Første", "Sidste", "MitUserName", "jensissa1999@gmail.com", 75453979, null, typeof(ValidationException))]
    public void UpdateUserInvalid(int id, string firstName, string lastName, string username, string email, int phoneNumber, string location, Type expectedMessage)
    {
        //Arrange
        PutUserDTO dto = new PutUserDTO
        {
            Id = id, FirstName = firstName, LastName = lastName, Location = location, Email = email,
            Username = username, BirthDay = new DateTime(2001, 10, 15), PhoneNumber = phoneNumber
        };
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        IUserService service =
            new UserService(mockRepo.Object, putUserValidator, postUserValiator, _mapper);
        //Act
        var action = () => service.UpdateUser(id, dto);
        //Assert
        var ex = Assert.Throws<ValidationException>(action);
        Assert.Equal(expectedMessage, ex.GetType());
    }
}