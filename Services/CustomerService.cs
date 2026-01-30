using Invoice_Manager_API.DTO.CustomerDTO;
using Invoice_Manager_API.Services.Interfaces;

namespace Invoice_Manager_API.Services;

public class CustomerService : ICustomerService
{
    public Task<CustomerResponseDto> CreateAsync(CustomerCreateRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResponseDto> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResponseDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResponseDto> UpdateAsync(CustomerUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
