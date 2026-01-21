using FacturaFacil.Core;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Net.Codecrete.QrCodeGenerator;
using System.Text.Json;
using System.Text;

namespace FacturaFacil.Services;


public class InvoiceDocument : IDocument
{
    private readonly InvoiceModel _invoiceRequest;

    public InvoiceDocument(InvoiceModel invoiceRequest)
    {
        _invoiceRequest = invoiceRequest;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(1, Unit.Centimetre);

            page.Header().Row(row =>
            {
                row.RelativeItem().Column(c =>
                {
                    c.Item().Text($"Factura #{_invoiceRequest.InvoiceNumber:D8}").SemiBold().FontSize(20);
                    c.Item().Text($"Punto de Venta: {_invoiceRequest.PointOfSale:D4}");
                });
                
                row.ConstantItem(50).Border(1).AlignCenter().Text(GetInvoiceLetter()).FontSize(24).Bold();
            });

            page.Content().PaddingVertical(1, Unit.Centimetre).Column(col =>
            {
                col.Item().Text($"Vendedor: {_invoiceRequest.Seller.Name} | CUIT: {_invoiceRequest.Seller.TaxId}");
                col.Item().Text($"Cliente: {_invoiceRequest.Buyer.Name} | CUIT: {_invoiceRequest.Buyer.TaxId}");
                col.Item().PaddingTop(10).LineHorizontal(1);

                // Tabla de ítems
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Descripción");
                        header.Cell().AlignRight().Text("Cant.");
                        header.Cell().AlignRight().Text("Precio Unit.");
                        header.Cell().AlignRight().Text("Total");
                    });

                    foreach (var item in _invoiceRequest.Item)
                    {
                        table.Cell().Text(item.Description);
                        table.Cell().AlignRight().Text(item.Quantity.ToString());
                        table.Cell().AlignRight().Text($"${item.UnitPrice:N2}");
                        table.Cell().AlignRight().Text($"${(item.Quantity * item.UnitPrice):N2}");
                    }
                });
            });

            page.Footer().Row(row =>
            {
                // QR Legal de AFIP a la izquierda
                row.ConstantItem(100).Column(c =>
                {
                    var qrUrl = GenerateAfipQrUrl();
                    var qr = QrCode.EncodeText(qrUrl, QrCode.Ecc.Medium);
                    var pngBytes = qr.ToPng(10, 0);
                    c.Item().Image(pngBytes);
                });

                // Datos de CAE a la derecha
                row.RelativeItem().AlignRight().Column(c =>
                {
                    c.Item().Text($"CAE: {_invoiceRequest.CaeNumber}").Bold();
                    c.Item().Text($"Vto. CAE: {_invoiceRequest.CaeDueDate:dd/MM/yyyy}");
                });

            });
        });
    }

    private string GetInvoiceLetter()
    {
        return _invoiceRequest.InvoiceType switch
        {
            1 => "A",
            6 => "B",
            11 => "C",
            _ => "?"
        };
    }

    private string GenerateAfipQrUrl()
    {
        var qrData = new
        {
            ver = 1,
            fecha = _invoiceRequest.IssueDate.ToString("yyyy-MM-dd"),
            cuit = long.Parse(_invoiceRequest.Seller.TaxId.Replace("-", "")),
            ptoVta = _invoiceRequest.PointOfSale,
            tipoCmp = _invoiceRequest.InvoiceType,
            nroCmp = _invoiceRequest.InvoiceNumber,
            importe = _invoiceRequest.Item.Sum(i => i.Quantity * i.UnitPrice),
            moneda = "PES",
            ctz = 1,
            tipoDocRec = 99,
            nroDocRec = 0,
            tipoCodAut = "E",
            codAut = long.Parse(_invoiceRequest.CaeNumber)
        };

        var json = JsonSerializer.Serialize(qrData);
        var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        return $"https://www.afip.gob.ar/fe/qr/?p={base64}";
    }
}