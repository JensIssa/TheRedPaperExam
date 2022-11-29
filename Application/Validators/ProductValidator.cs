using System.Data;
using Application.DTOs;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators;

public class ProductValidator
{
    
    public class PostProductValidator: AbstractValidator<PostProductDTO> 
    {
        public PostProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.ProductCondition).NotEmpty();
            RuleFor(p => p.ProductCondition).IsInEnum();
            RuleFor(p => p.Price).LessThanOrEqualTo(100000);
        }
    }
    
    public class PutProductValidator: AbstractValidator<PutProductDTO> 
    {
        public PutProductValidator()
        {
            RuleFor(p => p.productId).NotEmpty();
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.ProductCondition).NotEmpty();
            RuleFor(p => p.ProductCondition).IsInEnum();
            RuleFor(p => p.Price).LessThanOrEqualTo(100000);
        }
    }
}