using Application.DTOs;
using Application.Helpers;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;

namespace RedPaperUnitTest;

public class UserTest
{

    private UserValidator.UserPostValidator postUserValiator;
    private UserValidator.UserPutValidator putUserValidator;

    public UserTest()
    {
        postUserValiator = new UserValidator.UserPostValidator();
        putUserValidator = new UserValidator.UserPutValidator();
    }

    public static IEnumerable<Object[]> GetALlUsers_Test()
    {
        User user1 = new User{Id = 1, AssignedRole = Role.Customer, BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Esbjerg", Username = "JensIKørestol", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user2 = new User{Id = 2, AssignedRole = Role.Admin, BirthDay = new DateTime(2005, 10, 15 ), Email = "andreasberthelsen@hotmail.com", FirstName = "Andreas", Hash = " ", LastName = "Berthelsen",Username = "BerthelDenSeje", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user3 = new User{Id = 3, AssignedRole = Role.Admin, BirthDay = new DateTime(2007, 10, 15 ), Email = "mathiasmadsen@hotmail.com", FirstName = "Mathias", Hash = " ", LastName = "Madsen",Username = "MathiasDenEmo", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
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

    [Fact]
    public void CreateUserServiceTest()
    {
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        var putUserValidator = new UserValidator.UserPutValidator();
        var postUserValiator = new UserValidator.UserPostValidator();
        //Act
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator);
        //Assert
        Assert.NotNull(service);
        Assert.True(service is UserService);
    }

    [Theory]
    [MemberData(nameof(GetALlUsers_Test))]
    public void GetAllUsers(User[] data, List<User> expectedResult)
    {
        var fakerepo = data;

        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        //Act
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator);

        mockRepo.Setup(u => u.GetAllUsers()).Returns(fakerepo.ToList);

        var actualResult = service.GetAllUsers();
        
        Assert.Equal(expectedResult, actualResult);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actualResult));
        mockRepo.Verify(r => r.GetAllUsers(), Times.Once);
    }
    
    [Theory]
    [InlineData("JensIKørestol","JensIKørestol")]
    [InlineData("BerthelDenSeje","BerthelDenSeje")]
    [InlineData("MathiasDenEmo","MathiasDenEmo")]
    public void ValidGetUserByUsernameTest(string userUsername, string expectedResult)
    {
        User user1 = new User{Id = 1, AssignedRole = Role.Customer, BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Esbjerg", Username = "JensIKørestol", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user2 = new User{Id = 2, AssignedRole = Role.Admin, BirthDay = new DateTime(2005, 10, 15 ), Email = "andreasberthelsen@hotmail.com", FirstName = "Andreas", Hash = " ", LastName = "Berthelsen",Username = "BerthelDenSeje", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user3 = new User{Id = 3, AssignedRole = Role.Admin, BirthDay = new DateTime(2007, 10, 15 ), Email = "mathiasmadsen@hotmail.com", FirstName = "Mathias", Hash = " ", LastName = "Madsen",Username = "MathiasDenEmo", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};

        var fakeRepo = new List<User>();
        fakeRepo.Add(user1);
        fakeRepo.Add(user2);
        fakeRepo.Add(user3);
        
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        //Act
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator);

        mockRepo.Setup(u => u.GetUserByUsername(userUsername)).Returns(fakeRepo.Find(u => u.Username == userUsername));
        
        var actual = service.GetUserByUsername(userUsername);
        //Assert
        Assert.Equal(expectedResult, actual.Username);
        mockRepo.Verify(r => r.GetUserByUsername(userUsername), Times.Once);
    }

    [Theory]
    [InlineData("", "Username is empty or null")]
    [InlineData(null, "Username is empty or null")]
    public void InvalidGetUserByUsername(string username, string expectedExceptionMsg)
    {
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        //Act
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator);
        var ex = Assert.Throws<ArgumentException>(() => service.GetUserByUsername(username));
        //Assert
        Assert.Equal("Username is empty or null", ex.Message);
        mockRepo.Verify(r => r.GetUserByUsername(username), Times.Never);
    }
    
    /*
    /// <summary>
/// Doesn't work yet
/// </summary>
/// <param name="userId"></param>
    [Theory]
    [InlineData(1)] 
    public void CreateValidUser(int userId)
    {
        User user = new User{Id = userId, AssignedRole = Role.Customer, BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Issa", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        RegisterDTO dto = new RegisterDTO
        {
            AssingedRole = user.AssignedRole, Birthday = user.BirthDay, Email = user.Email, FirstName = user.FirstName,
            Password = user.Salt, LastName = user.LastName, location = user.Location,
            PhoneNumber = user.PhoneNumber
        };
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        var putUserValidator = new UserValidator.UserPutValidator();
        var postUserValiator = new UserValidator.UserPostValidator();
        //Act
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator);

        mockRepo.Setup(r => r.CreateNewUser(It.IsAny<User>())).Returns(user);

        var userCreated = service.CreateUser(dto);
        
        //Assert
        Assert.Equal(user.Id, userCreated.Id);
        Assert.Equal(user.FirstName, userCreated.Email);
        Assert.Equal(user.LastName, userCreated.LastName);
        Assert.Equal(user.Email, userCreated.Email);
        Assert.Equal(user.Location, userCreated.Location);
        Assert.Equal(user.AssignedRole, userCreated.AssignedRole);
        Assert.Equal(user.BirthDay, userCreated.BirthDay);
        Assert.Equal(user.PhoneNumber, userCreated.PhoneNumber);
        mockRepo.Verify(r=>r.CreateNewUser(It.IsAny<User>()), Times.Once);
    }

*/
    [Theory]
    [InlineData(1)] // Delete user with id 1 and expectedListSize 
    public void DeleteValidUserTest(int expectedListSize)
    {
        List<User> users = new List<User>();
        User toBeDeleted = new User{Id = 1, AssignedRole = Role.Customer, BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Issa", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};
        User user2 = new User{Id = 2, AssignedRole = Role.Customer, BirthDay = new DateTime(2001, 10, 15 ), Email = "jensissa@hotmail.com", FirstName = "Jens", Hash = " ", LastName = "Issa", Location = "Esbjerg", PhoneNumber = 12345678, Salt = " "};

        users.Add(toBeDeleted);
        users.Add(user2);
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        //Act
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator);

        mockRepo.Setup(r => r.GetAllUsers()).Returns(users);
        mockRepo.Setup(r => r.DeleteUser(toBeDeleted.Id)).Returns(() =>
        {
            users.Remove(toBeDeleted);
            return toBeDeleted;
        });
        var actual = service.DeleteUser(1);
        
        Assert.Equal(expectedListSize, users.Count);
        Assert.Equal(toBeDeleted, actual);
        Assert.DoesNotContain(toBeDeleted, users);
        mockRepo.Verify(r=> r.DeleteUser(1), Times.Once);
    }
    
    [Theory]
    [InlineData(-1, "ID does not exist or is null")]   
    [InlineData(0, "ID does not exist or is null")]    
    [InlineData(null, "ID does not exist or is null")] 
    public void DeleteInvalidUserTest(int userId, string expectedMessage)
    {
        // Arrange
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        //Act
        IUserService service = new UserService(mockRepo.Object, putUserValidator, postUserValiator);

        // Act 
        var action = () => service.DeleteUser(userId);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
        mockRepo.Verify(r=>r.DeleteUser(userId),Times.Never);
    }
    
}