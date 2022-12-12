using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs;
using Application.Helpers;
using Application.InterfaceRepos;
using Application.InterfaceServices;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly TokenGenerator _tokenGenerator;
    private readonly IValidator<PutUserDTO> _putValidator;

    public AuthService(IUserRepository userRepository,
        TokenGenerator tokenGenerator, IValidator<PutUserDTO> putValidator
    )
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _putValidator = putValidator;
    }

    public string Login(LoginDTO dto)
    {
        var user = _userRepository.GetUserByUsername(dto.Username);
        if (BCrypt.Net.BCrypt.Verify(dto.Password + user.Salt, user.Hash))
        {
            return _tokenGenerator.GenerateToken(user);
        }
        throw new Exception("Invalid login");
    }
    
    public string UpdatePassword(int userId, PutUserDTO dto)
    {
        try
        {
            var validate = _putValidator.Validate(dto);
            if (!validate.IsValid)
            {
                throw new ArgumentException(validate.ToString());
            }
            var user = _userRepository.GetUserByUsername(dto.Username);
            user.Hash = BCrypt.Net.BCrypt.HashPassword(dto.Password + user.Salt);
            _userRepository.UpdateUserPassword(userId, user);
            return _tokenGenerator.GenerateToken(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}