namespace Invoice_Manager_API.DTO.CustomerDTO;

public class CustomerUpdateRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}
