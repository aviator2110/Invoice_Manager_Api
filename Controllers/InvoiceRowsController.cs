using Invoice_Manager_API.Common;
using Invoice_Manager_API.DTO.CustomerDTO;
using Invoice_Manager_API.DTO.InvoiceRowDTO;
using Invoice_Manager_API.Models;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Manager_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceRowsController : ControllerBase
{
    private IInvoiceRowService _invoiceRowService;

    public InvoiceRowsController(IInvoiceRowService service)
    {
        this._invoiceRowService = service;
    }

    /// <summary>
    /// Retrieves all invoice rows associated with the specified invoice ID.
    /// </summary>
    /// <param name="id">The identifier of the invoice.</param>
    /// <returns>A list of invoice rows for the specified invoice.</returns>
    [HttpGet("/Invoice/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceRowResponseDto>>>> GetAllByInvoiceId([FromBody]int id)
    {
        var invoiceRows = await this._invoiceRowService.GetAllByInvoiceIdAsync(id);

        return Ok(
                ApiResponse<IEnumerable<InvoiceRowResponseDto>>
                    .SuccessResponse(invoiceRows, $"Invoice rows with invoice id {id} retrieved successfully")
            );
    }

    /// <summary>
    /// Retrieves a specific invoice row by its unique identifier.
    /// </summary>
    /// <param name="id">The identifier of the invoice row.</param>
    /// <returns>The invoice row if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<InvoiceRowResponseDto>>> GetById([FromBody] int id) 
    { 
        var invoiceRow = await this._invoiceRowService.GetByIdAsync(id);

        if (invoiceRow == null)
            return NotFound(
                    ApiResponse<InvoiceRowResponseDto>
                        .ErrorResponse($"Invoice row with id {id} did not found")
                    );

        return Ok(
                ApiResponse<InvoiceRowResponseDto>
                    .SuccessResponse(invoiceRow, "Invoice row successfully found")
            );
    }

    /// <summary>
    /// Creates a new invoice row.
    /// </summary>
    /// <param name="request">The invoice row creation request data.</param>
    /// <returns>The newly created invoice row with a 201 Created response.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<InvoiceRowResponseDto>>> Create([FromBody] InvoiceRowCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                ApiResponse<InvoiceRowResponseDto>
                    .ErrorResponse("Invalid request data")
            );
        }

        var createdInvoiceRow = await this._invoiceRowService.CreateAsync(request);

        return CreatedAtAction(
                nameof(GetById),
                new { id = createdInvoiceRow.Id },
                ApiResponse<InvoiceRowResponseDto>
                    .SuccessResponse(createdInvoiceRow, "Invoice row created successfully"));
    }

    /// <summary>
    /// Updates an existing invoice row by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the invoice row to update.</param>
    /// <param name="request">The updated invoice row data.</param>
    /// <returns>The updated invoice row if found; otherwise, a 404 Not Found response.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<InvoiceRowResponseDto>>> Update([FromBody] InvoiceRowUpdateRequest request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                ApiResponse<InvoiceRowResponseDto>
                    .ErrorResponse("Invalid request data")
            );
        }

        var updatedInvoiceRow = await this._invoiceRowService.UpdateAsync(id, request);

        if (updatedInvoiceRow == null)
            return NotFound(
                    ApiResponse<InvoiceRowResponseDto>
                        .ErrorResponse($"Invoice row with id {id} did not found")
                    );

        return Ok(
                ApiResponse<InvoiceRowResponseDto>
                    .SuccessResponse(updatedInvoiceRow, "Invoice row updated successfully")
            );
    }
}