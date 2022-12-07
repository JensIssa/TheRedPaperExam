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
            RuleFor(p => p.Price).LessThanOrEqualTo(100000);
            RuleFor(p => p.ProductConditionId).NotEmpty();

        }
    }
    
    public class PutProductValidator: AbstractValidator<PutProductDTO> 
    {
        public PutProductValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.ProductConditionId).NotEmpty();
            RuleFor(p => p.Price).LessThanOrEqualTo(100000);

        }
    }
}