using AutoMapper;
using Invoice_Manager_API.Common;
using Invoice_Manager_API.Data;
using Invoice_Manager_API.DTO.CustomerDTO;
using Invoice_Manager_API.Models;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Manager_API.Services;

public class CustomerService : ICustomerService
{
    private readonly InvoiceManagerApiDbContext _context;
    private readonly IMapper _mapper;

    public CustomerService(InvoiceManagerApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerResponseDto> CreateAsync(CustomerCreateRequest request)
    {
        Customer customer = this._mapper.Map<Customer>(request);

        this._context.Customers.Add(customer);

        await this._context.SaveChangesAsync();

        await this._context
                    .Entry(customer)
                    .Collection(c => c.Invoices)
                    .LoadAsync();

        return this._mapper.Map<CustomerResponseDto>(customer);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await this._context
                                .Customers
                                .FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);

        if (customer is null)
        {
            return false;
        }

        var hasInvoices = await _context.Invoices
            .AnyAsync(i => i.CustomerId == id);

        if (hasInvoices)
        {
            customer.DeletedAt = DateTimeOffset.UtcNow;
            await this._context.SaveChangesAsync();

            return true;
        }

        this._context.Customers.Remove(customer);

        await this._context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
    {
        var customers = await this._context
                                        .Customers
                                        .Where(c => c.DeletedAt == null)
                                        .ToListAsync();

        return this._mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
    }

    public async Task<CustomerResponseDto?> GetByIdAsync(int id)
    {
        var customer = await this._context.Customers
                                        .Where(c => c.Id == id && c.DeletedAt == null)
                                        .FirstOrDefaultAsync();

        if (customer is null)
        {
            return null;
        }

        return this._mapper.Map<CustomerResponseDto>(customer);
    }

    public async Task<PagedResult<CustomerResponseDto>> GetPagedAsync(CustomerQueryParams queryParams)
    {
        queryParams.Validate();

        var query = this._context
                        .Customers
                        .Include(c => c.Invoices)
                        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.Name))
            query = query.Where(c => c.Name.Contains(queryParams.Name));

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var searchTerm = queryParams.Search.ToLower();

            query = query.Where(c => c.Name.ToLower().Contains(searchTerm)
                                    || c.Address!.ToLower().Contains(searchTerm)
                                    || c.Email.ToLower().Contains(searchTerm)
                                    || c.PhoneNumber!.ToLower().Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Sort))
            query = ApplySorting(query, queryParams.Sort, queryParams.SortDirection!);
        else
            query = query.OrderByDescending(c => c.CreatedAt);

        var totalCount = await this._context.Customers.CountAsync();

        var maxPages = Convert.ToInt32(Math.Ceiling(totalCount / (double)queryParams.PageSize));
        if (queryParams.Page > maxPages)
            queryParams.Page = maxPages;

        var skip = (queryParams.Page - 1) * queryParams.PageSize;

        var resultCustomers = await query
                                    .Skip(skip)
                                    .Take(queryParams.PageSize)
                                    .ToListAsync();

        var customerDtos = this._mapper.Map<IEnumerable<CustomerResponseDto>>(resultCustomers);

        return PagedResult<CustomerResponseDto>.Create(
                                                        customerDtos,
                                                        queryParams.Page,
                                                        queryParams.PageSize,
                                                        totalCount
                                                    );
    }

    public async Task<CustomerResponseDto?> UpdateAsync(int id, CustomerUpdateRequest request)
    {
        var updatedCustomer = await this._context
                                        .Customers
                                        .Where(c => c.Id == id && c.DeletedAt == null)
                                        .FirstOrDefaultAsync();

        if (updatedCustomer is null)
        {
            return null;
        }

        this._mapper.Map(request, updatedCustomer);

        await _context.SaveChangesAsync();

        return this._mapper.Map<CustomerResponseDto>(updatedCustomer);
    }

    private IQueryable<Customer> ApplySorting(
                                        IQueryable<Customer> query,
                                        string sort,
                                        string sortDirection)
    {
        var isDescending = sortDirection?.ToLower() == "desc";

        return sort.ToLower() switch
        {
            "name" => isDescending
                            ? query.OrderByDescending(c => c.Name)
                            : query.OrderBy(c => c.Name),

            "createdat" => isDescending
                            ? query.OrderByDescending(c => c.CreatedAt)
                            : query.OrderBy(c => c.CreatedAt),

            "email" => isDescending
                            ? query.OrderByDescending(c => c.Email)
                            : query.OrderBy(c => c.Email),

            "address" => isDescending
                            ? query.OrderByDescending(c => c.Address)
                            : query.OrderBy(c => c.Address),

            _ => query.OrderByDescending(c => c.CreatedAt)
        };
    }
}