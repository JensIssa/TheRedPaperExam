using System.Security.Cryptography;
using Application.DTOs;
using Application.Helpers;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.TestHelper;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IValidator<PutUserDTO> _putValidator;
    

    public UserService(IUserRepository repository, IValidator<PutUserDTO> putValidator)
    {
        _repository = repository;
        _putValidator = putValidator;
    }

    public User GetUserByUsername(string username)
    {
        return _repository.GetUserByUsername(username);
    }

    public List<User> GetAllUsers()
    {
        return _repository.GetAllUsers();
    }
    public User UpdateUser(int id, PutUserDTO putUserDto)
    {
        if (id != putUserDto.Id)
        {
            throw new ValidationException("ID in body and route are different");
        }
        var validation = _putValidator.Validate(putUserDto);
        var user =  _repository.GetUserByUsername(putUserDto.Username);
        user.Hash = BCrypt.Net.BCrypt.HashPassword(putUserDto.Password + user.Salt);
        
        if (!validation.IsValid)
        {
            throw new ValidationTestException(validation.ToString());
        }

        return _repository.UpdateUser(user, id);
    }

    public User DeleteUser(int id)
    {
        return _repository.DeleteUser(id);
    }
}