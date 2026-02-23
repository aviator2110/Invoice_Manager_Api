using Invoice_Manager_API.Common;
using Invoice_Manager_API.DTO.InvoiceRowDTO;

namespace Invoice_Manager_API.Services.Interfaces;

public interface IInvoiceRowService
{
    Task<InvoiceRowResponseDto> CreateAsync(InvoiceRowCreateRequest request);
    Task<InvoiceRowResponseDto?> UpdateAsync(int id, InvoiceRowUpdateRequest request);
    Task<InvoiceRowResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<InvoiceRowResponseDto>> GetAllByInvoiceIdAsync(int id);
    Task<PagedResult<InvoiceRowResponseDto>> GetPagedAsync(InvoiceRowQueryParams queryParams);
}
