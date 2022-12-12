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
    }
    
    public UserService(IUserRepository repository, IValidator<PutUserDTO> putValidator, IValidator<RegisterDTO> postValidator)
    {
        _repository = repository;
        _putValidator = putValidator;
        _postValidator = postValidator;
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
        return CreateUser(dto, Role.Admin);
    }

    public User CreateCustomer(RegisterDTO dto)
    {
        return CreateUser(dto, Role.Customer);
    }
    
    private User CreateUser(RegisterDTO dto, Role role)
    {
        ExceptionHandlingPost(dto);
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
                AssignedRole = role
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
        ExceptionHandlingPut(putUserDto);
        if (id != putUserDto.Id)
        {
            throw new ValidationException("ID in body and route are different");
        }
        var salt = RandomNumberGenerator.GetBytes(32).ToString();
        var userToUpdate = new User()
        {
            Id = putUserDto.Id,
            Username = putUserDto.Username,
            Salt = salt,
            Hash = BCrypt.Net.BCrypt.HashPassword(putUserDto.Password + salt),
            FirstName = putUserDto.FirstName,
            LastName = putUserDto.LastName,
            BirthDay = putUserDto.BirthDay,
            Email = putUserDto.Email,
            PhoneNumber = putUserDto.PhoneNumber,
            Location = putUserDto.Location,
        };
        var validation = _putValidator.Validate(putUserDto);
        if (!validation.IsValid)
        {
            throw new ValidationTestException(validation.ToString());
        }
        return _repository.UpdateUser(_imapper.Map<User>(userToUpdate), id);
    }

    public User DeleteUser(int id)
    {
        if (id.Equals(null) || id < 1)
        {
            throw new ArgumentException("ID does not exist or is null");
        }
        return _repository.DeleteUser(id);
    }
    
    private void ExceptionHandlingPost(RegisterDTO user)
    {
        if (string.IsNullOrEmpty(user.FirstName)) throw new ArgumentException("First name cannot be null or empty");
        if (string.IsNullOrEmpty(user.LastName)) throw new ArgumentException("Last name cannot be null or empty");
        if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email cannot be null, nor empty");
        if (String.IsNullOrEmpty(user.PhoneNumber.ToString())) throw new ArgumentException("Work number cannot be null or empty ");
        if (string.IsNullOrEmpty(user.Location)) throw new ArgumentException("Email cannot be null, nor empty");
        if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email cannot be null, nor empty");
    }
    private void ExceptionHandlingPut(PutUserDTO user)
    {
        if (user.Id == null || user.Id < 1) throw new ArgumentException("Id cannot be null or less than 1");
        if (string.IsNullOrEmpty(user.FirstName)) throw new ArgumentException("First name cannot be null or empty");
        if (string.IsNullOrEmpty(user.LastName)) throw new ArgumentException("Last name cannot be null or empty");
        if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email cannot be null, nor empty");
        if (String.IsNullOrEmpty(user.PhoneNumber.ToString())) throw new ArgumentException("Work number cannot be null or empty ");
        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8) throw new ArgumentException("Password cannot be null, empty and must have a minimum length greater than 7");
        if (string.IsNullOrEmpty(user.Location)) throw new ArgumentException("Email cannot be null, nor empty");
        if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email cannot be null, nor empty");
    }
}