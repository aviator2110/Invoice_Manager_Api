using AutoMapper;
using Invoice_Manager_API.Common;
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

    public async Task<InvoiceRowResponseDto?> UpdateAsync(int id, InvoiceRowUpdateRequest request)
    {
        var updatedInvoiceRow = await this._context
                                            .InvoiceRows
                                            .Where(ir => ir.Id == id)
                                            .FirstOrDefaultAsync();

        if (updatedInvoiceRow == null)
            return null;

        this._mapper.Map(request, updatedInvoiceRow);

        await _context.SaveChangesAsync();

        return this._mapper.Map<InvoiceRowResponseDto>(updatedInvoiceRow);
    }

    public async Task<PagedResult<InvoiceRowResponseDto>> GetPagedAsync(InvoiceRowQueryParams queryParams)
    {
        queryParams.Validate();

        var query = this._context
                            .InvoiceRows
                            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.Service))
            query = query.Where(ir => ir.Service.Contains(queryParams.Service));

        if (queryParams.InvoiceId is not null)
            query = query.Where(ir => ir.InvoiceId ==  queryParams.InvoiceId);

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var searchTerm = queryParams.Search.ToLower();

            query = query.Where(ir => ir.Service.Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Sort))
            query = ApplySorting(query, queryParams.Sort, queryParams.SortDirection!);
        else
            query = query.OrderByDescending(i => i.Id);

        var totalCount = await this._context.InvoiceRows.CountAsync();

        var maxPages = Convert.ToInt32(Math.Ceiling(totalCount / (double)queryParams.PageSize));
        if (queryParams.Page > maxPages)
            queryParams.Page = maxPages;

        var skip = (queryParams.Page - 1) * queryParams.PageSize;

        var resultInvoiceRows = await query
                                    .Skip(skip)
                                    .Take(queryParams.PageSize)
                                    .ToListAsync();

        var invoiceRowDtos = this._mapper.Map<IEnumerable<InvoiceRowResponseDto>>(resultInvoiceRows);

        return PagedResult<InvoiceRowResponseDto>.Create(
                invoiceRowDtos,
                queryParams.Page,
                queryParams.PageSize,
                totalCount
            );
    }

    private IQueryable<InvoiceRow> ApplySorting(
                                        IQueryable<InvoiceRow> query,
                                        string sort,
                                        string sortDirection)
    {
        var isDescending = sortDirection?.ToLower() == "desc";

        return sort.ToLower() switch
        {
            "invoiceid" => isDescending
                            ? query.OrderByDescending(ir => ir.InvoiceId)
                            : query.OrderBy(ir => ir.InvoiceId),

            "quantity" => isDescending
                            ? query.OrderByDescending(ir => ir.Quantity)
                            : query.OrderBy(ir => ir.Quantity),

            "rate" => isDescending
                            ? query.OrderByDescending(ir => ir.Rate)
                            : query.OrderBy(ir => ir.Rate),

            "sum" => isDescending
                            ? query.OrderByDescending(ir => ir.Sum)
                            : query.OrderBy(ir => ir.Sum),

            _ => query.OrderByDescending(ir => ir.Id)
        };
    }
}