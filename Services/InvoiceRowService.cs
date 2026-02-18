using AutoMapper;
using Invoice_Manager_API.Data;
using Invoice_Manager_API.DTO.InvoiceRowDTO;
using Invoice_Manager_API.Models;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<InvoiceRowResponseDto> CreateAsync(InvoiceRowCreateRequest request)
    {
        var invoiceRow = this._mapper.Map<InvoiceRow>(request);

        this._context.InvoiceRows.Add(invoiceRow);

        await this._context.SaveChangesAsync();

        await this._context
            .Entry(invoiceRow)
            .Reference(ir => ir.Invoice)
            .LoadAsync();

        return this._mapper.Map<InvoiceRowResponseDto>(invoiceRow);
    }

    public async Task<IEnumerable<InvoiceRowResponseDto>> GetAllByInvoiceIdAsync(int id)
    {
        var invoiceRows = await this._context
                                        .InvoiceRows
                                        .Where(ir => ir.InvoiceId == id)
                                        .ToListAsync();

        return this._mapper.Map<IEnumerable<InvoiceRowResponseDto>>(invoiceRows);
    }

    public async Task<InvoiceRowResponseDto?> GetByIdAsync(int id)
    {
        var invoiceRow = await this._context
                                        .InvoiceRows
                                        .Where(ir => ir.Id == id)
                                        .FirstOrDefaultAsync();

        if (invoiceRow == null)
            return null;

        return this._mapper.Map<InvoiceRowResponseDto>(invoiceRow);
    }

    public async Task<InvoiceRowResponseDto?> UpdateAsync(int id, InvoiceRowCreateRequest request)
    {
        var updatedInvoiceRow = await this._context
                                            .InvoiceRows
                                            .Where(ir => ir.Id == id)
                                            .FirstOrDefaultAsync();

        if (updatedInvoiceRow == null)
            return null;

        this._mapper.Map(request, updatedInvoiceRow);

        return this._mapper.Map<InvoiceRowResponseDto>(updatedInvoiceRow);
    }
}