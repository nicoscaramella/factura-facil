using FacturaFacil.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Threading.Tasks;
using FacturaFacil.Core;

namespace FacturaFacil.Api.Functions;

public class GenerateInvoiceFunction
{
    private readonly PdfGenerationService _pdfGenerationService;

    public GenerateInvoiceFunction(PdfGenerationService pdfGenerator)
    {
        _pdfGenerationService = pdfGenerator;
    }

    [Function("GenerateInvoice")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Function, "post", "get", Route = "invoices")]
        HttpRequestData req)
    {
        // Diagnóstico: Permitir GET para verificar conectividad
        if (req.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
        {
            var pingResponse = req.CreateResponse(HttpStatusCode.OK);
            await pingResponse.WriteStringAsync("API Online: Conectividad exitosa.");
            return pingResponse;
        }

        var invoiceData = await req.ReadFromJsonAsync<InvoiceModel>();
        if (invoiceData == null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        byte[] pdfBytes = _pdfGenerationService.GeneratePdf(invoiceData);
        
        var response = req.CreateResponse(HttpStatusCode.Created);
        
        response.Headers.Add("Content-Type", "application/pdf");
        response.Headers.Add("Content-Disposition", $"attachment; filename=factura{invoiceData.InvoiceNumber}.pdf");
        
        await response.WriteBytesAsync(pdfBytes);
        
        return response;
    }
}