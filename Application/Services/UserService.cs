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
    private readonly IValidator<RegisterDTO> _postValidator;
    private readonly TokenGenerator _tokenGenerator;
    private readonly IMapper _imapper;
    
    public UserService(IUserRepository repository, IValidator<PutUserDTO> putValidator, TokenGenerator tokenGenerator, IValidator<RegisterDTO> postValidator, IMapper mapper)
    {
        _repository = repository;
        _putValidator = putValidator;
        _postValidator = postValidator;
        _tokenGenerator = tokenGenerator;
        _imapper = mapper;
        ExceptionHandlingConstructor();
    }
    
    public UserService(IUserRepository repository, IValidator<PutUserDTO> putValidator, IValidator<RegisterDTO> postValidator, IMapper mapper)
    {
        _repository = repository;
        _putValidator = putValidator;
        _postValidator = postValidator;
        _imapper = mapper;
        ExceptionHandlingConstructor();
    }

    public User GetUserByUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username is empty or null");
        }
        return _repository.GetUserByUsername(username);
    }

    public List<User> GetAllUsers()
    {
        return _repository.GetAllUsers();
    }

    public User CreateAdmin( RegisterDTO dto)
    {
        return CreateUser(dto, "Admin");
    }

    public User CreateCustomer(RegisterDTO dto)
    {
        return CreateUser(dto, "Customer");
    }
    
    private User CreateUser(RegisterDTO dto, string role)
    {
        try
        {
            _repository.GetUserByUsername(dto.Username);
        }
        catch (KeyNotFoundException)
        {
            var salt = RandomNumberGenerator.GetBytes(32).ToString();
            var user = new User
            {
                Username = dto.Username,
                Salt = salt,
                Hash = BCrypt.Net.BCrypt.HashPassword(dto.Password + salt),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDay = dto.Birthday,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Location = dto.Location,
                Role = role
            };
            var validation = _postValidator.Validate(dto);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.ToString());
            }
            _tokenGenerator.GenerateToken(user);
            return _repository.CreateNewUser(_imapper.Map<User>(user));

        }
        throw new Exception("Username " + dto.Username + " is already taken");
    }
    
    public User UpdateUser(int id, PutUserDTO putUserDto)
    {
        if (id != putUserDto.Id)
        {
            throw new ValidationException("ID in body and route are different");
        }
        var validation = _putValidator.Validate(putUserDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        return _repository.UpdateUser(_imapper.Map<User>(putUserDto), id);
    }
    
    public User DeleteUser(int id)
    {
        if (id.Equals(null) || id < 1)
        {
            throw new ArgumentException("ID does not exist or is null");
        }
        return _repository.DeleteUser(id);
    }
    public void ExceptionHandlingConstructor()
    {
        if (_repository == null)
        {
            throw new ArgumentException("This service cannot be constructed without a repository");
        }

        if (_imapper == null)
        {
            throw new ArgumentException("This service cannot be constructed without a mapper");
        }

        if (_putValidator == null )
        {
            throw new ArgumentException("This service cannot be constructed without a putValidator");
        }

        if (_postValidator == null)
        {
            throw new ArgumentException("This service cannot be constructed without a postValidator");
        }
    }
}