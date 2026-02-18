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
}