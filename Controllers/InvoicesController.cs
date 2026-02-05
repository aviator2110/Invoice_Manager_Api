using Invoice_Manager_API.Common;
using Invoice_Manager_API.DTO.InvoiceDTO;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Manager_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        this._invoiceService = invoiceService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceResponseDto>>>> GetAll()
    {
        var invoices = await this._invoiceService.GetAllAsync();

        return Ok(
            ApiResponse<IEnumerable<InvoiceResponseDto>>
                .SuccessResponse(invoices, "Invoices retrieved successfully")
        );
    }

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
}
