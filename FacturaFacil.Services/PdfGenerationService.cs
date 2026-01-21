using FacturaFacil.Core;
using QuestPDF.Fluent;
using QuestPDF.Companion;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;

namespace FacturaFacil.Services;

public class PdfGenerationService
{
        public byte[] GeneratePdf(InvoiceModel request)
        {
                var document = new InvoiceDocument(request);
                
                byte[] pdfBytes = document.GeneratePdf();
                
                return pdfBytes;
        }
}