using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class OrderValidator : AbstractValidator<PostOrderDTO>
{
    public OrderValidator()
    {
        RuleFor(o => o.UserId).NotEmpty().GreaterThan(0);
    }
    
}