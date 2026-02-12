using FluentValidation;
using Invoice_Manager_API.DTO.InvoiceDTO;

namespace Invoice_Manager_API.Validators;

public class UpdateInvoiceValidator : AbstractValidator<InvoiceUpdateRequest>
{
    public UpdateInvoiceValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("CustomerId must be greater than 0.");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("End date is required.")
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be greater than start date.");

        RuleFor(x => x.Comment)
            .MaximumLength(500)
            .WithMessage("Comment cannot exceed 500 characters.");
    }
}
