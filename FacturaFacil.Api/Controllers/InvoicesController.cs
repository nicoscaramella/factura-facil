using FacturaFacil.Core;
using FacturaFacil.Services;
using Microsoft.AspNetCore.Mvc;

namespace FacturaFacil.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly PdfGenerationService _pdfGenerationService;

    public InvoicesController(PdfGenerationService pdfGenerationService)
    {
        _pdfGenerationService = pdfGenerationService;
    }

    [HttpPost]
    public IActionResult GenerateInvoice([FromBody] InvoiceModel invoiceData)
    {
        if (invoiceData == null)
        {
            return BadRequest("Invoice data is required.");
        }

        try 
        {
            var pdfBytes = _pdfGenerationService.GeneratePdf(invoiceData);
            var fileName = $"factura-{invoiceData.InvoiceNumber}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }
        catch (Exception ex)
        {
            // Return full stack trace for debugging
            return StatusCode(500, $"Internal server error: {ex.Message}\nStack Trace: {ex.StackTrace}");
        }
    }
}
