using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CategoryValidator
{

    public class CategoryPostValidator : AbstractValidator<PostCategoryDTO>
    {
        public CategoryPostValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty(); 
        }
    }

    public class CategoryPutValidator : AbstractValidator<PutCategoryDTO>
    {
        public CategoryPutValidator()
        {
            RuleFor(c => c.CategoryName).NotNull().NotEmpty();
        }
    }

}