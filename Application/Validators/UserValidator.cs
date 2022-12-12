using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class UserValidator 
{

    public class UserPostValidator: AbstractValidator<RegisterDTO>
    {
        public UserPostValidator()
        {
            RuleFor(u => u.AssingedRole).NotNull();
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Birthday).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.PhoneNumber).NotNull();
            RuleFor(u => u.Location).NotEmpty();
        }
    }
    public class UserPutValidator: AbstractValidator<PutUserDTO>
    {
        public UserPutValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.BirthDay).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.PhoneNumber).NotEmpty();
            RuleFor(u => u.Location).NotEmpty();
        }
    }
    
    public class UserPutPasswordValidator: AbstractValidator<PutPasswordDTO>
    {
        public UserPutPasswordValidator()
        {
            RuleFor(u => u.Password).NotEmpty();
        }
    }
}