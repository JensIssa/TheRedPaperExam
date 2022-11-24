using Application.DTOs;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace RedPaperUnitTest;

public class UserTest
{
    
    [Fact]
    public void CreateUserServiceTest()
    {
        Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        var putUserValidator = new UserValidator.UserPutValidator();
        //Act
        IUserService service = new UserService(mockRepo.Object, putUserValidator);
        //Assert
        Assert.NotNull(service);
        Assert.True(service is UserService);
    }
    
    
}