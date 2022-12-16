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
            RuleFor(p => p.ProductName).NotEmpty().NotNull();
            RuleFor(p => p.ImageUrl).NotEmpty().NotNull();
            RuleFor(p => p.Description).NotEmpty().NotNull();
            RuleFor(p => p.Price).NotEmpty().NotNull();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.SubCategoryID).NotNull().GreaterThan(0);
            RuleFor(p => p.UserId).NotNull().GreaterThan(0);

        }
    }
    
    public class PutProductValidator: AbstractValidator<PutProductDTO> 
    {
        public PutProductValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();
            RuleFor(p => p.ProductName).NotEmpty().NotNull();
            RuleFor(p => p.ImageUrl).NotNull().NotEmpty();
            RuleFor(p => p.Description).NotNull().NotEmpty();
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
            RuleFor(p => p.ProductConditionId).NotEmpty().NotNull();

        }
    }
}