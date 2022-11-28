﻿using System.ComponentModel.DataAnnotations;
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
    private readonly IValidator<RegisterDTO> _postValidator;
    private readonly IMapper _mapper;
    private readonly TokenGenerator _tokenGenerator;
    public AuthService(IUserRepository userRepository, 
        IValidator<RegisterDTO> postValidator,
        IMapper mapper,
     TokenGenerator tokenGenerator 
    )
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _postValidator = postValidator;
        _mapper = mapper;
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
    
    
}