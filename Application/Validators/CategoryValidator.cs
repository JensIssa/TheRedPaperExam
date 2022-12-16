using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CategoryValidator
{

    public class CategoryPostValidator : AbstractValidator<PostCategoryDTO>
    {
        public CategoryPostValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty().NotNull(); 
        }
    }

    public class CategoryPutValidator : AbstractValidator<PutCategoryDTO>
    {
        public CategoryPutValidator()
        {
            RuleFor(c => c.Id).NotNull().GreaterThan(0);
            RuleFor(c => c.CategoryName).NotNull().NotEmpty();
        }
    }

}