using Invoice_Manager_API.Models;

namespace Invoice_Manager_API.DTO.InvoiceRowDTO;

public class InvoiceRowResponseDto
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public string Service { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Sum { get; set; }
}
