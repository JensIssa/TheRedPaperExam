using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class SubCategoryValidator
{
    public class PostSubCategoryValidator : AbstractValidator<PostSubCategoryDTO>
    {
        public PostSubCategoryValidator()
        {
            RuleFor(p => p.SubName).NotEmpty();
        }
    }

    public class PutSubCategoryValidator : AbstractValidator<PutSubCategoryDTO>
    {
        public PutSubCategoryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.SubName).NotEmpty();
        }
    }
}