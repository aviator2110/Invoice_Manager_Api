using Invoice_Manager_API.Common;
using Invoice_Manager_API.DTO.CustomerDTO;
using Invoice_Manager_API.DTO.InvoiceDTO;
using Invoice_Manager_API.Models;
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

    // Update


    // StatusUpdate

    // Delete

    // GetAll

    // GetById

}
