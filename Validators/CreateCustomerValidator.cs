using Invoice_Manager_API.DTO.CustomerDTO;
using FluentValidation;

namespace Invoice_Manager_API.Validators;

public class CreateCustomerValidator : AbstractValidator<CustomerCreateRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer Name is required")
            .MinimumLength(2).WithMessage("Customer Name must be at least 2 characters long");

        RuleFor(x => x.Email)
            .EmailAddress();
    }
}
