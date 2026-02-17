using Invoice_Manager_API.DTO.InvoiceRowDTO;

namespace Invoice_Manager_API.Services.Interfaces;

public interface IInvoiceRowService
{
    Task<InvoiceRowResponseDto> CreateAsync(InvoiceRowCreateRequest request);
    Task<InvoiceRowResponseDto?> UpdateAsync(InvoiceRowCreateRequest request);
    Task<bool> DeleteAsync(int id);
    Task<InvoiceRowResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<InvoiceRowResponseDto>> GetAllAsync();
}
