using AutoMapper;
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
        var customer = await this._context.Customers.FindAsync(id);

        if (customer is null)
        {
            return false;
        }

        this._context.Customers.Remove(customer);

        await this._context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
    {
        var customers = await this._context
                                        .Customers
                                        .Include(c => c.Invoices)
                                        .ToListAsync();

        return this._mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
    }

    public async Task<CustomerResponseDto?> GetByIdAsync(int id)
    {
        var customer = await this._context.Customers.FindAsync(id);

        if (customer is null)
        {
            return null;
        }

        return this._mapper.Map<CustomerResponseDto>(customer);
    }

    public async Task<CustomerResponseDto?> UpdateAsync(int id, CustomerUpdateRequest request)
    {
        var updatedCustomer = await this._context
                                        .Customers
                                        .Include(c => c.Invoices)
                                        .FirstOrDefaultAsync(c => c.Id == id);

        if (updatedCustomer is null)
        {
            return null;
        }

        this._mapper.Map(request, updatedCustomer);

        return this._mapper.Map<CustomerResponseDto>(updatedCustomer);
    }
}