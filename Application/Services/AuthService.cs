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
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly AppSettings _appSettings;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterDTO> _postValidator;
    private readonly IMapper _mapper;

    public AuthService(AppSettings appSettings, IUserRepository userRepository, IValidator<RegisterDTO> postValidator, IMapper mapper)
    {
        _appSettings = appSettings;
        _userRepository = userRepository;
        _postValidator = postValidator;
        _mapper = mapper;
    }

    public string Register(RegisterDTO dto)
    {
        try
        {
            _userRepository.GetUserByUsername(dto.Username);
        }
        catch (KeyNotFoundException)
        {
            var salt = RandomNumberGenerator.GetBytes(32).ToString();
            var user = new User
            {
                Username = dto.Username,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDay = dto.Birthday,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Location = dto.location,
                Salt = salt,
                Hash = BCrypt.Net.BCrypt.HashPassword(dto.Password + salt)
            };
            var validation = _postValidator.Validate(dto);
            if (!validation.IsValid)
            {
                throw new FluentValidation.ValidationException(validation.ToString());
            }
            _userRepository.CreateNewUser(_mapper.Map<User>(dto));
            return GenerateToken(user);
        }

        throw new Exception("Username " + dto.Username + " is already taken");
    }

    private string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[] { new Claim("username", user.Username) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public string Login(LoginDTO dto)
    {
        var user = _userRepository.GetUserByUsername(dto.Username);
        if (BCrypt.Net.BCrypt.Verify(dto.Password + user.Salt, user.Hash))
        {
            return GenerateToken(user);
        }

        throw new Exception("Invalid login");
    }
}