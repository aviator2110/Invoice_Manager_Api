using FluentValidation;
using Invoice_Manager_API.DTO.InvoiceRowDTO;

namespace Invoice_Manager_API.Validators;

public class CreateInvoiceRowValidator : AbstractValidator<InvoiceRowCreateRequest>
{
    public CreateInvoiceRowValidator()
    {
        RuleFor(x => x.InvoiceId)
            .GreaterThan(0)
            .WithMessage("InvoiceId must be greater than 0.");

        RuleFor(x => x.Service)
            .NotEmpty()
            .WithMessage("Service is required.")
            .MaximumLength(200)
            .WithMessage("Service cannot exceed 200 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.")
            .PrecisionScale(18, 2, true)
            .WithMessage("Quantity must have up to 18 digits in total and 2 decimal places.");

        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rate cannot be negative.")
            .PrecisionScale(18, 2, true)
            .WithMessage("Rate must have up to 18 digits in total and 2 decimal places.");
    }
}
