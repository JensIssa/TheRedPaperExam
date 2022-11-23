using System.Security.Cryptography;
using Application.DTOs;
using Application.Helpers;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<PutUserDTO> _putValidator;
    private readonly TokenGenerator _tokenGenerator;

    public UserService(IUserRepository repository, IMapper mapper, IValidator<PutUserDTO> putValidator, TokenGenerator tokenGenerator)
    {
        _repository = repository;
        _mapper = mapper;
        _putValidator = putValidator;
        _tokenGenerator = tokenGenerator;
    }

    public User GetUserByUsername(string username)
    {
        return _repository.GetUserByUsername(username);
    }

    public List<User> GetAllUsers()
    {
        return _repository.GetAllUsers();
    }

    public string UpdateUser(int id, PutUserDTO putUserDto)
    {
        if (id != putUserDto.Id)
        {
            throw new ValidationException("ID in body and route are different");
        }
        var salt = RandomNumberGenerator.GetBytes(32).ToString();
        var user = new User
        {
            Username = putUserDto.Username,
            FirstName = putUserDto.FirstName,
            LastName = putUserDto.LastName,
            BirthDay = putUserDto.BirthDay,
            Email = putUserDto.Email,
            PhoneNumber = putUserDto.PhoneNumber,
            Location = putUserDto.Location,
            Salt = salt,
            Hash = BCrypt.Net.BCrypt.HashPassword(putUserDto.Password + salt)
        };
        var validation = _putValidator.Validate(putUserDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        _repository.UpdateUser(_mapper.Map<User>(putUserDto), id);
        return _tokenGenerator.GenerateToken(user);
    }

    public User DeleteUser(int id)
    {
        return _repository.DeleteUser(id);
    }
}