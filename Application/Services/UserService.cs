using Application.DTOs;
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
    private readonly IValidator<RegisterDTO> _postValidator;
    private readonly IValidator<PutUserDTO> _putValidator;
    
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
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        return _repository.UpdateUser(_mapper.Map<User>(putUserDto), id);
    }

    public User DeleteUser(int id)
    {
        return _repository.DeleteUser(id);
    }
}