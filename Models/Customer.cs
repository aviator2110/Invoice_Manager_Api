namespace Invoice_Manager_API.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; } // Обновляется при любом изменении сущности
    public DateTimeOffset? DeletedAt { get; set; } // Используеться для soft delete

    public List<Invoice> Invoices { get; set; } = new List<Invoice>();
}
