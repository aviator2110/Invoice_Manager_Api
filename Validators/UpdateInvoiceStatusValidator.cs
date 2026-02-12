using FluentValidation;
using Invoice_Manager_API.DTO.InvoiceDTO;

namespace Invoice_Manager_API.Validators;

public class UpdateInvoiceStatusValidator : AbstractValidator<InvoiceStatusUpdateRequest>
{
    public UpdateInvoiceStatusValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid invoice status.");
    }
}
