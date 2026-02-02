using Invoice_Manager_API.Models;

namespace Invoice_Manager_API.DTO.InvoiceRowDTO;

public class InvoiceRowUpdateRequest
{
    public int InvoiceId { get; set; }
    public string Service { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
}
