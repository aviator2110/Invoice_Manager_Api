using Invoice_Manager_API.Models;

namespace Invoice_Manager_API.DTO.InvoiceDTO;

public class InvoiceUpdateRequest
{
    public int CustomerId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string? Comment { get; set; }
}
