using Invoice_Manager_API.Models;

namespace Invoice_Manager_API.DTO.InvoiceDTO;

public class InvoiceCreateRequest
{
    public int CustomerId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public List<InvoiceRow> Rows { get; set; } = new();
    public string? Comment { get; set; }
    public InvoiceStatus Status { get; set; }
}
