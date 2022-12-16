using System.Data;
using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class SubCategoryValidator
{
    public class PostSubCategoryValidator : AbstractValidator<PostSubCategoryDTO>
    {
        public PostSubCategoryValidator()
        {
            RuleFor(p => p.SubName).NotEmpty().NotNull();
            RuleFor(p => p.CategoryID).NotNull().GreaterThan(0);
        }
    }

    public class PutSubCategoryValidator : AbstractValidator<PutSubCategoryDTO>
    {
        public PutSubCategoryValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(0);
            RuleFor(p => p.SubName).NotEmpty().NotNull();
            RuleFor(s => s.CategoryID).NotNull().GreaterThan(0);
        }
    }
}