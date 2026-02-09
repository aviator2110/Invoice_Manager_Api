using Invoice_Manager_API.DTO.InvoiceRowDTO;
using Invoice_Manager_API.Models;

namespace Invoice_Manager_API.DTO.InvoiceDTO;

public class InvoiceResponseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public List<InvoiceRowResponseDto> Rows { get; set; } = new();
    public decimal TotalSum { get; set; }
    public string? Comment { get; set; }
    public InvoiceStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
