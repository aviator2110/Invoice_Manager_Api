namespace Invoice_Manager_API.Models;

public class InvoiceRow
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public string Service { get; set; } = null!; // Название выполненной работы
    public decimal Quantity { get; set; } // Количество единиц
    public decimal Rate { get; set; } // Цена за единицу
    public decimal Sum { get; set; } // Quantity * Rate

    public Invoice Invoice { get; set; } = null!;
}
