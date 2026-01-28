namespace Invoice_Manager_API.Models;

public class Invoice
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTimeOffset StartDate { get; set; } // Начало периода выполнения работ
    public DateTimeOffset EndDate { get; set; } // Конец периода выполнения работ
    public List<InvoiceRow> Rows { get; set; } = new();
    public decimal TotalSum { get; set; } // Сумма всех Rows.Sum
    public string? Comment { get; set; }
    public InvoiceStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; } // Обновляется при любом изменении
    public DateTimeOffset? DeletedAt { get; set; } // Soft delete
}

public enum InvoiceStatus
{
    Created,
    Sent,
    Received,
    Paid,
    Cancelled,
    Rejected
}