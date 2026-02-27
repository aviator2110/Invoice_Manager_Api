using Invoice_Manager_API.Common;
using Invoice_Manager_API.DTO.CustomerDTO;
using Invoice_Manager_API.DTO.InvoiceDTO;
using Invoice_Manager_API.Services;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Manager_API.Controllers;

/// <summary>
/// Invoice Controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private IInvoiceService _invoiceService;

    /// <summary>
    /// Invoice Controller constructor.
    /// </summary>
    public InvoicesController(IInvoiceService invoiceService)
    {
        this._invoiceService = invoiceService;
    }

    /// <summary>
    /// Retrieves a list of all invoices in the system.
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceResponseDto>>>> GetAll()
    {
        var invoices = await this._invoiceService.GetAllAsync();

        return Ok(
            ApiResponse<IEnumerable<InvoiceResponseDto>>
                .SuccessResponse(invoices, "Invoices retrieved successfully")
        );
    }

    /// <summary>
    /// Retrieves a paginated list of invoices based on the specified query parameters.
    /// </summary>
    /// <param name="queryParams">
    /// The filtering, sorting, and pagination parameters for retrieving invoices.
    /// </param>
    /// <returns>
    /// A paginated result containing invoices that match the provided query parameters.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<ApiResponse<PagedResult<IEnumerable<InvoiceResponseDto>>>>> GetPaged([FromQuery] InvoiceQueryParams queryParams)
    {
        var result = await this._invoiceService.GetPagedAsync(queryParams);

        return Ok(
            ApiResponse<PagedResult<InvoiceResponseDto>>
                .SuccessResponse(result, "Invoices retrieved successfully")
        );
    }

    /// <summary>
    /// Updates an existing invoice with the specified identifier using the provided request data.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> Update([FromBody] InvoiceUpdateRequest request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                ApiResponse<InvoiceResponseDto>
                    .ErrorResponse("Invalid request data")
            );
        }

        var updatedInvoice = await this._invoiceService.UpdateAsync(request, id);

        if (updatedInvoice is null) 
        {
            return NotFound(
                ApiResponse<InvoiceResponseDto>
                    .ErrorResponse($"Invoice with ID {id} not found")
            );
        }

        return Ok(
            ApiResponse<InvoiceResponseDto>
                .SuccessResponse(updatedInvoice, "Invoice updated successfully")
        );
    }

    /// <summary>
    /// Updates the status of an existing invoice with the specified identifier.
    /// </summary>
    [HttpPut("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> StatusUpdate([FromBody] InvoiceStatusUpdateRequest request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                ApiResponse<InvoiceResponseDto>
                    .ErrorResponse("Invalid request data")
            );
        }

        var updatedInvoice = await this._invoiceService.StatusUpdateAsync(request, id);

        if (updatedInvoice is null)
        {
            return NotFound(
                ApiResponse<InvoiceResponseDto>
                    .ErrorResponse($"Invoice with ID {id} not found")
            );
        }

        return Ok(
            ApiResponse<InvoiceResponseDto>
                .SuccessResponse(updatedInvoice, "Invoice status updated successfully")
        );
    }

    /// <summary>
    /// Deletes an invoice with the specified identifier using a soft delete mechanism.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var isDeleted = await this._invoiceService.DeleteAsync(id);

        if (isDeleted == false)
        {
            return NotFound(
                ApiResponse<InvoiceResponseDto>
                    .ErrorResponse($"Invoice with ID {id} not found")
            );
        }

        return Ok(
            ApiResponse<InvoiceResponseDto>
                .SuccessResponse(null, "Invoice deleted successfully")
        );
    }

    /// <summary>
    /// Retrieves an invoice by the specified identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> GetById(int id)
    {
        var invoice = await this._invoiceService.GetByIdAsync(id);

        if (invoice is null)
        {
            return NotFound(
                ApiResponse<InvoiceResponseDto>
                    .ErrorResponse($"Invoice with ID {id} not found")
            );
        }

        return Ok(
            ApiResponse<InvoiceResponseDto>
                .SuccessResponse(invoice, "Invoice found")
        );
    }

    /// <summary>
    /// Creates a new invoice based on the provided request data.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> Create([FromBody] InvoiceCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                ApiResponse<InvoiceResponseDto>
                    .ErrorResponse("Invalid request data")
            );
        }

        var createdInvoice = await this._invoiceService.CreateAsync(request);

        return CreatedAtAction(
                nameof(GetById),
                new { id = createdInvoice.Id },
                ApiResponse<InvoiceResponseDto>
                    .SuccessResponse(createdInvoice, "Invoice created successfully"));
    }
}
