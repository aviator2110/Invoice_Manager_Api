using Invoice_Manager_API.DTO.InvoiceDTO;
using Invoice_Manager_API.Services.Interfaces;

namespace Invoice_Manager_API.Services;

public class InvoiceService : IInvoiceService
{
    public Task<InvoiceResponseDto> CreateAsync(InvoiceCreateRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<InvoiceResponseDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceResponseDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceResponseDto> StatusDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceResponseDto> StatusUpdateAsync(InvoiceStatusUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceResponseDto> UpdateAsync(InvoiceUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
