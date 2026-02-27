using FluentValidation;
using Invoice_Manager_API.DTO.AuthDTO;

namespace Invoice_Manager_API.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name required")
            .MinimumLength(2).WithMessage("User first name must be at least 2 characters long");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name required")
            .MinimumLength(2).WithMessage("User last name must be at least 2 characters long");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("User password must be at least 6 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Password must contain at least one uppercase letter, one lowercase letter and one digital symbol");

        RuleFor(x => x.ConfirmedPassword)
            .NotEmpty().WithMessage("Password is required")
            .Equal(x => x.Password).WithMessage("Passwords don't match");
    }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email required")
                .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("User password must be at least 6 characters long")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Password must contain at least one uppercase letter, one lowercase letter and one digital symbol");
    }
}