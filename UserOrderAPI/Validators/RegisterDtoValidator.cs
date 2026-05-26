using FluentValidation;
using UserOrderAPI.DTOs;

namespace UserOrderAPI.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name).
                Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3);

            RuleFor(x => x.Email).
                NotEmpty().EmailAddress().
                WithMessage("A valid email is required.");

            RuleFor(x => x.Password).
                NotEmpty().
                MinimumLength(6).
                WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.Role).NotEmpty();
        }
    }
}
