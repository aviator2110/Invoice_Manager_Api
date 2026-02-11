using FluentValidation;
using Invoice_Manager_API.DTO.CustomerDTO;

namespace Invoice_Manager_API.Validators;

public class UpdateCustomerValidator : AbstractValidator<CustomerUpdateRequest>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer Name is required")
            .MinimumLength(2).WithMessage("Customer Name must be at least 2 characters long");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adress is required")
            .MinimumLength(4).WithMessage("Adress must be at least 4 characters long");

        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Invalid phone number format");
    }
}
