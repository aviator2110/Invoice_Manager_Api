using Invoice_Manager_API.Common;
using Invoice_Manager_API.DTO.CustomerDTO;
using Invoice_Manager_API.DTO.InvoiceDTO;

namespace Invoice_Manager_API.Services.Interfaces;

public interface IInvoiceService
{
	Task<InvoiceResponseDto> CreateAsync(InvoiceCreateRequest request);
	Task<InvoiceResponseDto?> UpdateAsync(InvoiceUpdateRequest request, int id);
	Task<InvoiceResponseDto?> StatusUpdateAsync(InvoiceStatusUpdateRequest request, int id);
	Task<bool> DeleteAsync(int id);
	Task<IEnumerable<InvoiceResponseDto>> GetAllAsync();
	Task<InvoiceResponseDto?> GetByIdAsync(int id);
    Task<PagedResult<InvoiceResponseDto>> GetPagedAsync(InvoiceQueryParams queryParams);
}