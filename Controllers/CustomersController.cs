using Invoice_Manager_API.Common;
using Invoice_Manager_API.DTO.CustomerDTO;
using Invoice_Manager_API.Models;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Manager_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        this._customerService = customerService;
    }

    /// <summary>
    /// Retrieves a list of all customers in the system.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<CustomerResponseDto>>>> GetAll()
    {
        var customers = await this._customerService.GetAllAsync();

        return Ok(
            ApiResponse<IEnumerable<CustomerResponseDto>>
                .SuccessResponse(customers, "Customers retrieved successfully")
        );
    }

    /// <summary>
    /// Retrieves a customer by the specified identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> GetById(int id)
    {
        var customer = await this._customerService.GetByIdAsync(id);

        if (customer is null)
        {
            return NotFound(
                ApiResponse<CustomerResponseDto>
                    .ErrorResponse($"Customer with ID {id} not found")
            );
        }

        return Ok(
            ApiResponse<CustomerResponseDto>
                .SuccessResponse(customer, "Customer found")
        );
    }

    /// <summary>
    /// Creates a new customer based on the provided request data.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> Create([FromBody] CustomerCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                ApiResponse<CustomerResponseDto>
                    .ErrorResponse("Invalid request data")
            );
        }

        var createdCustomer = await this._customerService.CreateAsync(request);

        return CreatedAtAction(
                nameof(GetById),
                new { id = createdCustomer.Id },
                ApiResponse<CustomerResponseDto>
                    .SuccessResponse(createdCustomer, "Customer created successfully"));
    }

    /// <summary>
    /// Updates an existing customer with the specified identifier using the provided request data.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> Update([FromBody] CustomerUpdateRequest request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                ApiResponse<CustomerResponseDto>
                    .ErrorResponse("Invalid request data")
            );
        }

        var updatedCustomer = await this._customerService.UpdateAsync(id, request);

        if (updatedCustomer is null)
        {
            return NotFound(
                ApiResponse<CustomerResponseDto>
                    .ErrorResponse($"Customer with ID {id} not found")
            );
        }

        return Ok(
            ApiResponse<CustomerResponseDto>
                .SuccessResponse(updatedCustomer, "Customer updated successfully")
        );
    }

    /// <summary>
    /// Deletes a customer with the specified identifier using a soft delete mechanism.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var isDeleted = await this._customerService.DeleteAsync(id);

        if (isDeleted == false)
        {
            return NotFound(
                ApiResponse<CustomerResponseDto>
                    .ErrorResponse($"Customer with ID {id} not found")
            );
        }

        return Ok(
            ApiResponse<CustomerResponseDto>
                .SuccessResponse(null, "Customer deleted successfully")
        );
    }
}