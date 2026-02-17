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


}