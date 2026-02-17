using AutoMapper;
using Invoice_Manager_API.Data;
using Invoice_Manager_API.DTO.InvoiceRowDTO;
using Invoice_Manager_API.Models;
using Invoice_Manager_API.Services.Interfaces;

namespace Invoice_Manager_API.Services;

public class InvoiceRowService : IInvoiceRowService
{
    private InvoiceManagerApiDbContext _context;
    private readonly IMapper _mapper;
    public InvoiceRowService(InvoiceManagerApiDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public Task<InvoiceRowResponseDto> CreateAsync(InvoiceRowCreateRequest request)
    {
        var invoiceRow = this._mapper.Map<InvoiceRow>(request);


    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<InvoiceRowResponseDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceRowResponseDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceRowResponseDto?> UpdateAsync(InvoiceRowCreateRequest request)
    {
        throw new NotImplementedException();
    }
}