using AutoMapper;
using Invoice_Manager_API.Common;
using Invoice_Manager_API.Data;
using Invoice_Manager_API.DTO.CustomerDTO;
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
        invoice.Status = InvoiceStatus.Created;
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
            .Where(i => i.DeletedAt == null)
            .Include(i => i.Rows)
            .ToListAsync();

        return this._mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices);
    }

    public async Task<InvoiceResponseDto?> GetByIdAsync(int id)
    {
        var invoice = await this._context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

        if (invoice is null)
        {
            return null;
        }

        return this._mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var invoice = await _context.Invoices
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

        if (invoice is null)
        {
            return false;
        }

        if (invoice.Status != InvoiceStatus.Created)
        {
            invoice.DeletedAt = DateTimeOffset.UtcNow;
        }
        else
        {
            this._context.Invoices.Remove(invoice);
        }

        await this._context.SaveChangesAsync();
        return true;
    }

    public async Task<InvoiceResponseDto?> StatusUpdateAsync(InvoiceStatusUpdateRequest request, int id)
    {
        var updatedInvoice = await this._context
            .Invoices
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

        if (updatedInvoice is null)
        {
            return null;
        }

        this._mapper.Map(request, updatedInvoice);

        await _context.SaveChangesAsync();

        return this._mapper.Map<InvoiceResponseDto>(updatedInvoice);
    }

    public async Task<InvoiceResponseDto?> UpdateAsync(InvoiceUpdateRequest request, int id)
    {
        var updatedInvoice = await this._context
            .Invoices
            .Include(i => i.Rows)
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

        if (updatedInvoice is null || updatedInvoice.Status != InvoiceStatus.Created)
        {
            return null;
        }

        this._mapper.Map(request, updatedInvoice);

        updatedInvoice.TotalSum = updatedInvoice.Rows.Sum(r => r.Sum);
        await _context.SaveChangesAsync();

        return this._mapper.Map<InvoiceResponseDto>(updatedInvoice);
    }

    public async Task<PagedResult<InvoiceResponseDto>> GetPagedAsync(InvoiceQueryParams queryParams)
    {
        queryParams.Validate();

        var query = this._context
                        .Invoices
                        .Include(i => i.Rows)
                        .AsQueryable();

        if (queryParams.CustomerId is not null)
            query = query.Where(i => i.CustomerId == queryParams.CustomerId);

        if (!string.IsNullOrWhiteSpace(queryParams.Status))
            query = query.Where(i => i.Status.ToString() == queryParams.Status);

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var searchTerm = queryParams.Search.ToLower();

            query = query.Where(i => i.Comment!.Contains(searchTerm)
                                || i.Status.ToString().Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Sort))
            query = ApplySorting(query, queryParams.Sort, queryParams.SortDirection!);
        else
            query = query.OrderByDescending(i => i.CreatedAt);

        var totalCount = await this._context.Invoices.CountAsync();

        var maxPages = Convert.ToInt32(Math.Ceiling(totalCount / (double)queryParams.PageSize));
        if (queryParams.Page > maxPages)
            queryParams.Page = maxPages;

        var skip = (queryParams.Page - 1) * queryParams.PageSize;

        var resultInvoices = await query
                                    .Skip(skip)
                                    .Take(queryParams.PageSize)
                                    .ToListAsync();

        var invoiceDtos = this._mapper.Map<IEnumerable<InvoiceResponseDto>>(resultInvoices);

        return PagedResult<InvoiceResponseDto>.Create(
                                                    invoiceDtos,
                                                    queryParams.Page,
                                                    queryParams.PageSize,
                                                    totalCount
                                                    );   
    }

    private IQueryable<Invoice> ApplySorting(
                                        IQueryable<Invoice> query,
                                        string sort,
                                        string sortDirection)
    {
        var isDescending = sortDirection?.ToLower() == "desc";

        return sort.ToLower() switch
        {
            "customerid" => isDescending
                            ? query.OrderByDescending(i => i.CustomerId)
                            : query.OrderBy(i => i.CustomerId),

            "createdat" => isDescending
                            ? query.OrderByDescending(i => i.CreatedAt)
                            : query.OrderBy(i => i.CreatedAt),

            "startdate" => isDescending
                            ? query.OrderByDescending(i => i.StartDate)
                            : query.OrderBy(i => i.StartDate),

            "enddate" => isDescending
                            ? query.OrderByDescending(i => i.EndDate)
                            : query.OrderBy(i => i.EndDate),

            "totalsum" => isDescending
                            ? query.OrderByDescending(i => i.TotalSum)
                            : query.OrderBy(i => i.TotalSum),

            "status" => isDescending
                            ? query.OrderByDescending(i => i.Status)
                            : query.OrderBy(i => i.Status),

            _ => query.OrderByDescending(i => i.CreatedAt)
        };
    }
}
