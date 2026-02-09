using AutoMapper;
using Invoice_Manager_API.Data;
using Invoice_Manager_API.DTO.InvoiceDTO;
using Invoice_Manager_API.Models;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Manager_API.Services;

public class InvoiceService : IInvoiceService
{
    private readonly InvoiceManagerApiDbContext _context;
    private readonly IMapper _mapper;

    public InvoiceService(InvoiceManagerApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InvoiceResponseDto> CreateAsync(InvoiceCreateRequest request)
    {
        Invoice invoice = this._mapper.Map<Invoice>(request);

        invoice.TotalSum = invoice.Rows.Sum(r => r.Sum);

        this._context.Invoices.Add(invoice);

        await this._context.SaveChangesAsync();

        await this._context
            .Entry(invoice)
            .Collection(i => i.Rows)
            .LoadAsync();

        await this._context
            .Entry(invoice)
            .Reference(i => i.Customer)
            .LoadAsync();

        return this._mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<IEnumerable<InvoiceResponseDto>> GetAllAsync()
    {
        var invoices = await this._context
            .Invoices
            .Include(i => i.Customer)
            .Include(i => i.Rows)
            .ToListAsync();

        return this._mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices);
    }

    public async Task<InvoiceResponseDto?> GetByIdAsync(int id)
    {
        var invoice = await this._context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (invoice is null)
        {
            return null;
        }

        return this._mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var invoice = await this._context.Invoices.FindAsync(id);

        if (invoice is null)
        {
            return false;
        }

        this._context.Invoices.Remove(invoice);

        await this._context.SaveChangesAsync();

        return true;
    }

    public async Task<InvoiceResponseDto?> StatusUpdateAsync(InvoiceStatusUpdateRequest request, int id)
    {
        var updatedInvoice = await this._context
            .Invoices
            .Include(i => i.Rows)
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (updatedInvoice is null)
        {
            return null;
        }

        this._mapper.Map(request, updatedInvoice);

        return this._mapper.Map<InvoiceResponseDto>(updatedInvoice);
    }

    public async Task<InvoiceResponseDto?> UpdateAsync(InvoiceUpdateRequest request, int id)
    {
        var updatedInvoice = await this._context
            .Invoices
            .Include(i => i.Rows)
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (updatedInvoice is null)
        {
            return null;
        }

        updatedInvoice.TotalSum = updatedInvoice.Rows.Sum(r => r.Sum);

        this._mapper.Map(request, updatedInvoice);

        return this._mapper.Map<InvoiceResponseDto>(updatedInvoice);
    }
}
