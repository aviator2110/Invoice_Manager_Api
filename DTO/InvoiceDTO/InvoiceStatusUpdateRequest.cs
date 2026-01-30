using Invoice_Manager_API.Models;

namespace Invoice_Manager_API.DTO.InvoiceDTO;

public class InvoiceStatusUpdateRequest
{
    public InvoiceStatus Status { get; set; }
}
