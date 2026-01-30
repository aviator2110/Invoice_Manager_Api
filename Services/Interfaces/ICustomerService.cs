using Invoice_Manager_API.DTO.CustomerDTO;

namespace Invoice_Manager_API.Services.Interfaces;

public interface ICustomerService
{
	Task<CustomerResponseDto> CreateAsync(CustomerCreateRequest request);
	Task<CustomerResponseDto> UpdateAsync(CustomerUpdateRequest request);
	Task<CustomerResponseDto> DeleteAsync(int id);
	Task<IEnumerable<CustomerResponseDto>> GetAllAsync();
	Task<CustomerResponseDto> GetByIdAsync(int id);
}
