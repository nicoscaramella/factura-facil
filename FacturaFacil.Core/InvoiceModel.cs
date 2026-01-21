namespace FacturaFacil.Core;

public class InvoiceModel
{
    public int InvoiceNumber { get; set; }
    public int PointOfSale { get; set; }
    public int InvoiceType { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public string CaeNumber { get; set; } = string.Empty;
    public DateTime CaeDueDate { get; set; }
    
    public CompanyInfo Seller { get; set; } = new();
    public CompanyInfo Buyer { get; set; } = new();
    public List<InvoiceItem> Item { get; set; } = new();
}

public class CompanyInfo
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
}

public class InvoiceItem
{
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}