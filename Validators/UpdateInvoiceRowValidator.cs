using FluentValidation;
using Invoice_Manager_API.DTO.InvoiceRowDTO;

namespace Invoice_Manager_API.Validators;

public class UpdateInvoiceRowValidator : AbstractValidator<InvoiceRowUpdateRequest>
{
    public UpdateInvoiceRowValidator()
    {
        RuleFor(x => x.InvoiceId)
            .GreaterThan(0);

        RuleFor(x => x.Service)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .PrecisionScale(18, 2, true);

        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(0)
            .PrecisionScale(18, 2, true);
    }
}
